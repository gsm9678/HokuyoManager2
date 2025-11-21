# HokuyoManager2
Program for easy and convenient use of 2D LiDAR sensors made by Hokuyo  
Easily set the sensor area and send the relative position value through the OSC  
Sensor used during development test : UST-10LX

# 프로그램 개요

## 프로그램 목적

\-Hokuyo 센서를 간편하게 센서 영역을 설정하기 위한 프로그램
# 센서 IP 찾기 및 지정

## 프로그램 설치

<https://sourceforge.net/projects/urgbenri/>

해당 링크에서 프로그램 다운로드  
<img width="555" height="232" alt="image" src="https://github.com/user-attachments/assets/103e9253-cefe-43e8-ac9c-9beb97fcc088" />  
<img width="596" height="23" alt="image" src="https://github.com/user-attachments/assets/923a4c51-c530-4b78-aa09-3c8ed9eb3570" />  

다운로드 된 Installer.exe 실행  
<img width="234" height="119" alt="image" src="https://github.com/user-attachments/assets/8b205b42-b2b8-44c2-aaba-6a91a904d91b" />  
<img width="449" height="350" alt="image" src="https://github.com/user-attachments/assets/f9cd94cd-2b7b-403d-9084-07ed265a9cb7" />
<img width="445" height="348" alt="image" src="https://github.com/user-attachments/assets/add57632-a936-46b9-820e-e6d2bd9976db" />
<img width="454" height="348" alt="image" src="https://github.com/user-attachments/assets/e8337df2-265d-4144-b40b-b7e096b4bd38" />
<img width="437" height="337" alt="image" src="https://github.com/user-attachments/assets/40542fde-30af-4250-8a80-f0e0c613f6e9" />



## 프로그램 사용 방법

- **센서 Ip찾기**  
     <img width="467" height="187" alt="image" src="https://github.com/user-attachments/assets/2e9899dc-5d4f-4182-8115-b5c4825ca6c4" />  
     <img width="547" height="379" alt="image" src="https://github.com/user-attachments/assets/3a2e8d4f-4732-4fd5-8616-72088f23d240" />  


한 번에 못 찾는 경우가 있어 Search버튼을 여러 번 눌러 확인

지속해서 센서를 인식하지 못한다면 센서가 알맞게 설치되었는지 확인

- **IP 변경**

- 1. Data source의 IP 주소에 IP주소를 변경하고 싶은 센서의 IP주소를 입력 후 연결 버튼을 눌러 센서와 연결
     <img width="480" height="170" alt="image" src="https://github.com/user-attachments/assets/31f76e8c-4a65-41a1-b76d-7d09b3a93438" />

- 2. 오른쪽 아래의 바를 아래로 내려 Change IP Address 버튼을 클릭, 그 후 나오는 메시지에 Yes 버튼 클릭
     <img width="492" height="295" alt="image" src="https://github.com/user-attachments/assets/452dacbf-0b87-4a62-88bb-bec98eabc2bb" />  
     <img width="223" height="116" alt="image" src="https://github.com/user-attachments/assets/61491b3f-e0a9-4014-b4bc-b25b1b6fd79f" />

- 3. 변경하고 싶은 IP를 입력 후 Update 클릭, 그 후 나오는 팝업에 Yes 버튼 클릭  
     <img width="315" height="168" alt="image" src="https://github.com/user-attachments/assets/80101875-0274-495f-a24a-a0ce450c15c4" />  
     <img width="353" height="122" alt="image" src="https://github.com/user-attachments/assets/a32571a3-5145-494b-af3f-81ce612f929b" />

- 4. 만약에 오류가 발생하면 OK버튼을 누른 후 ③번 과정을 반복  
      <img width="377" height="134" alt="image" src="https://github.com/user-attachments/assets/ada466b0-166c-4977-b500-75c3f5c7811a" />  


- 5. 해당 확인 메시지가 나오면 Yes버튼을 눌러 설정을 완료  
  <img width="353" height="136" alt="image" src="https://github.com/user-attachments/assets/4d9ca2b3-0adf-4476-b830-4b67f70d1f73" />


# 프로그램 설치 및 설정

## 프로그램 설치

\-빌드한 프로그램을 바탕화면에 이동  
<img width="438" height="323" alt="image" src="https://github.com/user-attachments/assets/d4b7f7c6-4d43-47e1-94ac-1c9719f747f6" />

\-HokuyoManager2.exe 파일 실행

## 프로그램 설정  
<img width="925" height="521" alt="image" src="https://github.com/user-attachments/assets/e9ae5278-57fd-4d2c-ae03-e6dcc689c952" />

- **센서 구역 설정**  
  <img width="300" height="272" alt="image" src="https://github.com/user-attachments/assets/f053e463-2f7e-48f1-af85-9295a53e34a7" />
  - **Create:** 새로운 센서 설정 생성
  - **Dropdown:** 생성된 센서 설정 선택
  - **Disconnect:** 선택한 센서의 연결 해제
  - **Connect:** 선택한 센서의 연결
  - **Delete:** 선택한 센서를 삭제
  - **Zoom In/Out:** 각 포인트의 간격 조절 (1 - 20)
  - **X Position:** 센서의 중심의 X 값을 이동 (-600 - 600)
  - **Y Position:** 센서의 중심의 Y 값을 이동 (-600 - 600)
  - **Rotate Camera:** 센서의 중심을 회전 (-180 - 180)
  - **X Flip:** 센서를 X축으로 플립
  - **Y Flip:** 센서를 Y 축으로 플립
- **방 크기 설정**  
  <img width="282" height="57" alt="image" src="https://github.com/user-attachments/assets/be4f16e0-dfdc-40a1-89b7-a764d02f6795" />
  - **X Size:** 영역의 X사이즈 설정 (50 - 800)
  - **Y Size:** 영역의 Y사이즈 설정 (50 - 800)
- **센서 민감도 설정**  
  <img width="281" height="55" alt="image" src="https://github.com/user-attachments/assets/1a748b56-7c50-44d3-aa95-66e735e4a9af" />
  - **Epslion:** 포인트간에 인식할 거리 설정(5 - 30)
  - **Min Point:** Epslion값 안에 군집으로 인식할 포인트 최소 숫자 (1 - 20)
- **무시 영역 설정**  
  <img width="297" height="202" alt="image" src="https://github.com/user-attachments/assets/7f472835-e6c0-438e-8523-84e5ba79cc6e" />
  - **Create:** 무시 영역 생성
  - **Dropdown:** 생성된 무시영역을 선택
  - **X Position:** 선택된 무시영역의 X값 이동 (-600 - 600)
  - **Y Position:** 선택된 무시영역의 Y값 이동 (-600 - 600)
  - **X Size:** 선택된 무시영역의 X 사이즈 설정 (1 - 800)
  - **Y Size:** 선택된 무시영역의 Y 사이즈 설정 (1 - 800)
  - **Delete:** 선택된 무시영역 삭제
- **OSC 연결 설정**  
  <img width="263" height="153" alt="image" src="https://github.com/user-attachments/assets/e62d4f07-1921-4869-b430-7025b28836f8" />
  - **Disconnect:** 센서 및 OSC 연결 해제
  - **Connect:** 센서 및 OSC 연결
  - **OSC IP Address:** 연결할 OSC의 IP 주소 설정
  - **OSC Port:** 연결할 OSC의 Port 주소 설정
  - **OSC Address:** 값을 전달할 OSC의 메시지 주소 설정
- **영역 설정 상황 GUI**  
  <img width="263" height="153" alt="image" src="https://github.com/user-attachments/assets/23dc25b4-ab54-4162-b809-05ab4d358bc6" />
  - **회색 바탕:** 영역 크기, 센서 구역 설정의 X size, Y size로 크기 설정
  - **색 네모:** 영역안에 들어간 센서의 포인트, 센서 구역 설정의 Point Scale로 크기 설정
  - **빨강색 네모:** 인식된 객체의 중심점, 센서 구역 설정의 Min Scale, Max Scale로 크기 설정
  - **검은색 네모:** 무시 영역, 무시 영역 설정으로 설정 가능 
- **Save**
  - **Save버튼:** JSON으로 데이터 저장

# 설치 노하우

\-모든 세팅이 끝난 후 Save 필수

## OSC 연결 노하우

<img width="266" height="153" alt="image" src="https://github.com/user-attachments/assets/e4ab63ec-4a88-437f-9604-b8d589de5b24" />

- OSC IP Address는 Hokuyo Manager2와 연결할 프로그램이 설치 되어있는 컴퓨터의 주소 입력, 만약 같은 PC라면 127.0.0.1로 입력
- OSC Port는 최종 목적지의 포트번호 입력(기본: 7000)
- OSC Address는 해당 센서의 위치에 따라 값 입력

| 바닥  | Down |
| --- | --- |
| 앞쪽 벽 | Front |
| 오른쪽 벽 | Right |
| 왼쪽 벽 | Left |
| 뒤쪽 벽 | Back |

## 센서 구역 설정 노하우

- 센서를 사용할 정확한 구역이 정해진 다음 센서 구역 설정 진행
- 센서 구역의 끝에 벽 또는 물체를 세우면 쉽게 구역 설정 가능  
  1. X Size와 Y Size를 최대 크기로 설정

     <img width="202" height="276" alt="image" src="https://github.com/user-attachments/assets/fafda03e-6a19-4e87-a8c8-6563755adba9" />

  2. Create 버튼을 눌러 새로운 센서 설정을 만든다.

     <img width="202" height="276" alt="image" src="https://github.com/user-attachments/assets/de224571-6723-4e43-a7bd-8f00548a435a" />

  3. Hokuyo IP Address를 연결할 센서의 IP 주소로 지정 사용할 IP를 찾는 방법은 2.2 IP찾기 참조 IP를 정확하게 입력했으면 Connect를 눌러 센서와 통신 시작

     <img width="263" height="241" alt="image" src="https://github.com/user-attachments/assets/44e328e1-ee9e-40e0-9f63-a0750eb3f761" />

  4. Zoom In/Out, X Position, Y Position 값을 조절하여 센서를 설정해 놓은 맵 사이즈에 들어오게 한다. (실제 위치와 동일)

     <img width="525" height="296" alt="image" src="https://github.com/user-attachments/assets/3bf5d665-77aa-435d-9290-0b5e562a367e" />

  5. Rotate Camera를 값과 X Flip, Y Flip을 이용하여 센서의 알맞은 방향으로 설정, 이때 센서가 맵 안에 전부 안 들어온다면 4번 과정을 다시 수행

     <img width="523" height="293" alt="image" src="https://github.com/user-attachments/assets/0a654f5e-d47b-4dd3-9731-598a18ebba8e" />

  6. 벽 또는 기준이 되는 물체가 맵에서 아슬하게 인식하지 않을 때까지 X Size, Y Size를 줄이고 X Position, Y Position의 값을 조절한다.

      <img width="537" height="302" alt="image" src="https://github.com/user-attachments/assets/88d6b6e9-442d-439e-8dc0-d2365344f838" />

  7. 만약 여러 개의 센서를 사용한다면 2~5번을 원하는 만큼 수행  
  8. Epslion값을 조절하여 군집을 만들 거리를 설정하고 Min Point값을 조절하여 군집의 최소 숫자를 설정한다.

      <img width="537" height="302" alt="image" src="https://github.com/user-attachments/assets/f7f49f3a-7627-4a57-a63b-6f3858c6a715" />


## 무시영역 설정

<img width="598" height="332" alt="image" src="https://github.com/user-attachments/assets/1f7d7462-b24d-41e6-8084-66f5fd8c3835" />

- 바닥 또는 벽이 평탄하지 않아 원하지 않은 곳이 인식을 하거나 일정한 곳에 튀는 값이 지속될 경우
- 게임 구역에는 포함되나 센서를 인식하지 않은 공간을 만들고 싶은 경우
- Create 버튼을 누르고 X Size, Y Size를 설정한 뒤 X Position, Y Position을 이동시켜 원하는 공간에 놔둔다.
