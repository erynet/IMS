using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;

using IMS.Server.Common;

namespace IMS.Server.Host
{
    public class Core : IDisposable
    {
        public readonly string NameSpace = "IMS.Server.Host.Core";
        private readonly string _currentEntryAssemblyPath;
        private readonly List<ISubComponent> _subComponentList; 
        private readonly BusHub _busHub;

        private bool _loopContinue;

        // 외부로 로그 메시지 등을 전달하는 통로로 쓰이는 이벤트 핸들러
        public event EventHandler Log;

        // 생성자
        public Core()
        {
            // 먼저 현재 실행중인 어셈블리의 폴더(exe)의 경로를 찾는다.
            _currentEntryAssemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _subComponentList = new List<ISubComponent>();
            _busHub = new BusHub();
            _loopContinue = true;
        }

        public bool Attach(string libName = null)
        {
            Regex rx;
            
            // 현재 경로에 존재하는 모든 파일들의 목록을 얻어온다.
            string[] fileNames = Directory.GetFiles(_currentEntryAssemblyPath);

            // 얻어온 파일들의 목록중 Data 에 해당하는 dll 파일명을 얻어오고 네임스페이스를 저장한다.
            if (libName == null)
            {
                rx = new Regex(@"(IMS.Server.Sub.\w+).dll", 
                    RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                rx = new Regex($@"(IMS.Server.Sub.{libName}.\w+).dll", 
                    RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            var assemblyCandidates = (from file in fileNames
                                         where rx.IsMatch(file)
                                         orderby file
                                         select new
                                         {
                                             fullpath = file,
                                             filename = rx.Match(file).Groups[0].Value,
                                             ns = rx.Match(file).Groups[1].Value
                                         }).ToList();
            if (!assemblyCandidates.Any())
                return false;


            foreach (var candidate in assemblyCandidates)
            {
                try
                {
                    ISubComponent _subComponent = 
                        new DynamicInvoker(Assembly.LoadFrom(candidate.fullpath).GetType(candidate.ns + ".Entry")).
                            CreateInstance() as ISubComponent;
                    if (_subComponent == null)
                        return false;
                    _subComponent.ConnectBus(_busHub);
                    _subComponent.Log += SubComponentLogEvtConsumer;
                    _subComponentList.Add(_subComponent);
                    L($@"Subcomponent loaded `{candidate.filename}`", LogEvt.MessageType.Info);
                }
                catch (Exception e)
                {
                    L($@"Error occured in loading `{candidate.filename}`", LogEvt.MessageType.Warning);
                    string msg = e.ToString();
                    L(msg, LogEvt.MessageType.Error);
                    return false;
                }
            }
            
            return true;
        }

        public void Run()
        {
            if (_subComponentList.Any(subComponent => !subComponent.Initialize(0)))
            {
                return;
            }
            L("Every subcomponents initialized", LogEvt.MessageType.Info);

            while (_loopContinue)
            {
                Thread.Sleep(100);
            }

            foreach (var subComponent in _subComponentList)
            {
                subComponent.Dispose();
            }
        }
        
        public void Dispose()
        {
            L("Terminate Service", LogEvt.MessageType.Info);
            _loopContinue = false;
        }

        private void SubComponentLogEvtConsumer(object sender, EventArgs e)
        {
            Log?.Invoke(this, e);
        }

        public void L(string message, LogEvt.MessageType type = LogEvt.MessageType.Comment)
        {
            Log?.Invoke(this, new LogEvt(message, type, new StackTrace(), NameSpace));
        }
    }
}
