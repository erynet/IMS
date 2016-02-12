// 클라 -> 서버
최초 부팅()
이벤트 팝업 띄워주는 시간 저장(startTime, endTime)
UPS 장비 사용 여부 변경(int upsID)
그룹 사용 여부 변경(int groupID, bool use)
그룹 내 UPS 장비 개별 사용 가능 여부 변경(int groupID, bool use)
분전반 사용 여부 변경(int groupID, bool use)
접점 이름 변경(int 접점 번호, string name)
UPS 장비 추가(string name, string ip, string 설치일, string 배터리 사양, int 배터리 용량)
UPS 장비 1개 정보 요청(int upsID)
UPS 장비 1개 로그 요청(int upsID)
데이터 저장 시간 변경(int hours)
전력량 전송 주기 변경(int minutes)
// 서버 -> 클라
최초 부팅 시에만 받는 정보(필요하면)
이벤트 팝업 띄워주는 시간 변경(시작 시간, 종료 시간)
모든 UPS 장비의 간략 정보 리스트(사용 여부, 번호, 소속 그룹 번호, 소속 그룹 이름, 번호, 짝 UPS 번호, 장비 명칭, 배터리 용량, 배터리 사양, IP, 설치일, 좌표)
모든 그룹의 간략 정보 리스트(사용 여부, 번호, 그룹 내 장비 개별 사용 여부, 그룹 이름)
모든 분전반 간략 정보 리스트(사용 여부, 번호, 이름, 소속 UPS들 번호, IP, 설치일)
UPS 장비 1개 정보(너무 많으니 생략)
UPS 장비 1개 로그(생략)
// 기타
장비 상태 - on, off, error