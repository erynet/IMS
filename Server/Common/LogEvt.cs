using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IMS.Server.Common
{
    public class LogEvt : EventArgs
    {
        public enum MessageType { Comment = 0, Info = 1, Debug = 2, Warning = 3, Error = 4 };

        public string Message { get; }

        public DateTime When { get; }

        public int Depth => GetStackTrace.FrameCount;

        public MessageType Category { get; }

        public StackTrace GetStackTrace { get; }

        public string From { get; }

        public List<string> StackTraces
        {
            get
            {
                List<string> s = new List<string>();
                for (int i = GetStackTrace.FrameCount; i >= 0; i--)
                {
                    var sf = GetStackTrace.GetFrame(i);
                    StringBuilder sb = new StringBuilder();

                    var method = sf.GetMethod();
                    sb.Append(method.DeclaringType.ToString());
                    sb.Append(".");
                    sb.Append(method.Name);

                    var parameters = method.GetParameters();
                    sb.Append("(");
                    for (int j = 0; j < parameters.Length; ++j)
                    {
                        if (j > 0)
                            sb.Append(", ");
                        var parameter = parameters[j];
                        sb.Append(parameter.ParameterType.Name);
                        sb.Append(" ");
                        sb.Append(parameter.Name);
                    }
                    sb.Append(")");

                    var sourceFileName = sf.GetFileName();
                    if (!string.IsNullOrEmpty(sourceFileName))
                    {
                        sb.Append(" in ");
                        sb.Append(sourceFileName);
                        sb.Append(": line ");
                        sb.Append(sf.GetFileLineNumber().ToString());
                    }

                    s.Add(sb.ToString());
                }

                return s;
            }
        }

        public LogEvt(string message, MessageType messageType, StackTrace st, string from)
        {
            When = DateTime.Now;

            Message = message;
            Category = messageType;
            GetStackTrace = st;
            From = from;
        }

        public LogEvt(string message, StackTrace st, string from) : this(message, MessageType.Comment, st, from) { }

    }
}
