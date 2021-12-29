# chatbot
# 2021 AI Challenge   
SIML 쳇봇

# 팀 소개
팀명 : 미파고    
팀원 : 이영직, 신용준, 이상화, 고은현  
주제 : SIML 쳇봇 만들기


# 목차
1. 연구 소개
2. 구현 기능
3. 유니티 project 만들기
4. 결론

# 연구 소개
> chatbot이란?  
* 기업용 메신저에 채팅하듯 질문을 입력하면 인공지능(AI)이 빅데이터 분석을 바탕으로 일상언어로 사람과 대화를 하며 해답을 주는 대화형 메신저를 말한다.

* 챗봇은 크게 인공지능형과 시나리오형으로 나뉜다.
시나리오형은 미리 정해 놓은 단어에 따라 정해진 답을 내놓기 때문에 보안 위험이 그리 크지 않다.
인공지능형 챗봇은 복잡한 질문에도 응답할 수 있고 자기학습도 가능하다. 하지만 이용자의 입력 단어에 의도치 않게 행동해 개인정보 유출, 피싱, 해킹 같은 보안 위협에 취약한 것으로 평가된다.

> 연구 목적
1. 쳇봇의 동작 원리 이해하기
2. 연세대학교 미래캠퍼스에 대한 질문에 대해 대답하는 쳇봇 만들기
3. 쳇봇에 대한 답변을 유니티와 연결하여 입모양을 움지여 소리내어 읽어주기


# 구현 기능
> 구현 기능
1. 쳇봇의 동작원리를 살펴볼 수 있었다.
2. 데이터 셋에 설정한 키워드가 들어간 질문에 대한 답변을 확인 할 수 있었다.
3. 유니티를 이용해 캐릭터와 쳇봇은 연결함. 
4. 답변을 유니티 캐릭터가 소리내어 읽어주며 입모양이 움직임.


>  추가 연구 계획
1. 추가적인 데이터 셋을 증가시켜 더 많은 키워드를 설정해준다.  
2. 캐릭터 보다 더 향상된 디지털 휴면과 연동시키기.  


# 유니티 project 만들기
● 버전은 2019.4.17.f1 버전으로 사용한다.
● 개인 PC의 검색창 -> unity hub -> 프로젝트 -> 새로 생성 -> 템플릿(3D), 프로젝트 이름(HelloBot_Unity), 저장 위치(HelloBot 폴더 선택)
1. UI > Canvas와 Scroll View 추가(Canvas에 Scroll View가 속하도록)
2. Scroll View > viewpoint > Content의 Anchor를 left-bottom으로 설정
3. Content에서, Add Component > Content Size Fitter, Vertical Layout Group추가.
4. 좌측 하단에 Assets 폴더에 Prefabs 폴더 생성. 이후 방금까지의 ‘Text’ gameobject를 prefabs 폴더에 드래그하여 prefab으로 저장.
5. Canvas에서 우클릭하여 UI > InputField 추가. 다시 우클릭하여 UI > Button 추가. Button의 text는 ‘Send’로 설정.
6. https://syn.co.in/download/Syn.Bot.Demo.UnityPackage 다음 주소에서 유니티와 Syn Bot Demo를 연결 시켜줄 패키지 다운.
7. 좌측 상단에 Assets > Import Package > Custom Package를 통해 다운받은 패키지를 import한다.
8. Hierarchy에서 ctrl + shift + N 눌러서 GameObject 생성 후 이름을 ChatManager로 변경.
9. Assets 폴더에 Scripts 폴더 생성 후 거기서 [ChatManager] C# Script를 생성

10. 스크립트를 아래와 같이 채운다.

![image](https://user-images.githubusercontent.com/91588673/147665273-fbb424b9-aa27-4894-bb41-db65074c7a7f.png)
![image](https://user-images.githubusercontent.com/91588673/147665278-7fe9cd7a-828c-4c08-9b6d-6814fae27840.png)
![image](https://user-images.githubusercontent.com/91588673/147665292-b9b6e0ff-8d47-4d9d-9cc3-c9e5c01d1f6a.png)

11. ChatManager의 Start()를 아래와 같이 코드 변경 

![image](https://user-images.githubusercontent.com/91588673/147665321-1cbe309e-f650-4f7b-86d5-8d508e76ac94.png)

12. BotDialog 클래스 생성

![image](https://user-images.githubusercontent.com/91588673/147665333-e1d8769d-b220-4a89-8be6-605aee399f9c.png)

13. Simlpk 파일을 유니티에 이식(+ ChatManager 스크립트 수정) 후 실행하기

![image](https://user-images.githubusercontent.com/91588673/147665350-6631242c-ad3c-4a29-8e37-a02c37a278bf.png)
![image](https://user-images.githubusercontent.com/91588673/147665360-0d57d783-c405-432f-97b8-3fb1f5f9a1db.png)
![image](https://user-images.githubusercontent.com/91588673/147665369-17199861-cc3f-484d-a6a0-10ef2eb1bde7.png)
![image](https://user-images.githubusercontent.com/91588673/147665381-bba58da4-a377-438b-9544-947378c3b83e.png)
![image](https://user-images.githubusercontent.com/91588673/147665389-5709841c-ed0b-48d7-9152-d945be56c3de.png)


<아바타 적용시키기>
1. 아바터 적용전 지금껏 만든 Unity를 백업(Export)한다. Assets > Export Package
2. HelloBot_Animation 이름의 새로운 Unity Project를 생성.
3. Assets > Import Package > Custom Package > All > Import 백업한 패키지 불러오기
4. 좌측 상단 Window > Asset Store > LipSync-animal collections 검색, 구매 및 Import
5. animal collections 폴더 > Prefabs > Animal heads > Animal heads1, Animal heads1의 이름을 head로 변경.

 ![image](https://user-images.githubusercontent.com/91588673/147665446-af6c5388-56c6-4960-8b9c-e2fcb38a6c2c.png)
 
6. Pronunciation_Kor_En.cs를 Assets 폴더 내로 위치하기.(업로드 된 자료에 있다.)
7. Pronunciation_Kor_En.cs를 ChatManager에 드래그하여 Component를 추가한다.
8. ChatManager.cs 수정

![image](https://user-images.githubusercontent.com/91588673/147665545-45bd3af1-e586-4cdf-8520-9ea5c70e98ae.png)
![image](https://user-images.githubusercontent.com/91588673/147665552-66a39ef7-ecc3-482c-b81b-98bfa9088d04.png)
![image](https://user-images.githubusercontent.com/91588673/147665559-ba4f9996-9b51-4b18-8cd6-f6824dfda59e.png)

9. TextToSpeech.cs 수정

![image](https://user-images.githubusercontent.com/91588673/147665575-3b2a4bd3-4d61-4199-94a0-478bc94dafb1.png)
![image](https://user-images.githubusercontent.com/91588673/147665580-50c2e302-f319-4859-b918-0c614ef1be04.png)

10. LipSyncManager.cs 수정

 ![image](https://user-images.githubusercontent.com/91588673/147665597-2864a214-7991-4142-b37d-11b4d107c049.png)
 
11. 최종 결과

 ![image](https://user-images.githubusercontent.com/91588673/147665619-d8cda1c5-a6f9-4a3b-97f0-591cbd20b2bb.png)


# 결론
쳇봇에게 질문에대한 키워드를 설정 해 두었고, 해당 키워드가 들어오면 미리 지정해 놓은 답변을 내놓는 것에 성공을 하였다. 그리고 그것과 유니티를 연관시켜 답변을 내놓을 때 소리내어 읽어 주었고,
동시에 입모양을 움직이는 것을 확인 할 수 있었다.

> 실험 결과 분석
유니티와 연결된 캐릭터가 소리내어 답변을 읽어주는 것에 성공함 초기에 목적을 달성 할 수 있었다.

>느낀점
실제로 수업시간에 배운 것을 직접 적용해봄으로써 단순히 이론만 배운 것이 아닌, 배운 지식이 어떻게 이용될 수 있나 배울 수 있었다. 또한 조별과제로 진행되었기 때문에 서로에 능력에 맞게
어려움을 헤쳐나가는 기술에 대해 배울 수 있었다. 이렇게 좋은 기회를 주신 윤한얼 교수님, 이창재 조교님, 황서림 조교님께 존경의 표합니다.


# Reference  
'Bert와GPT로 배우는 자연어 처리 '
'https://terms.naver.com/entry.naver?docId=3536264&cid=42107&categoryId=42107'
'Supervised and helped by C-J Lee, S-R Hwang, and H-U Yoon'
