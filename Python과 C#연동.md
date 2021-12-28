# Python과 C#연동

## 개발 과정

* `Unity`와 `SIML`을 사용한 챗봇 개발을 위해 이후 기술할 `KoNLPy`를 사용하기 위해서는 `python`과` C#`의 연동이 필요 하였다. 다음은 시도한 방법들이다.
  * `IronPython` (Python3 지원 하지 않음)
  * `Pythonnet` (socket networking에 비해 어려운 환경설정)
  * Socket Networking (비교적 간단함)

* Socket Networking
  * 앞서 나열한 방법들 중 첫번째, 두번째는 다른 환경 변수에 의해 크게 영향을 많이 받고, 업데이트에 따른 문제들이 항상 많이 발생하였고 앞으로도 많이 발생할것이다.
  * 이에반해, 소켓네트워킹은 `C#`과 `Python`간의 연결을 Library에 의존하는 것이 아닌 OS 자체를 통해 해결되므로 각 언어의 환경에 의존하지 않아도 된다.
* 서버의 역할과 기능은?
  * 우리가 사용할 서버는 매우 간단하다.
  * 기존의 파이썬 기반의 소켓 채팅 서버를 약간 감미료를 첨가하여 맛깔나게 만들었다.
  * 서버와 클라이언트는 소켓을 통해서 대화한다. 클라이언트인 유니티는 화면 입력으로부터 질문 받은 문장을 소켓을 통해 서버인` python`으로 보낸다. 
  * 서버에서는 `KoNLPy`가 문장을 적당히 단어들로 잘라낸다. 이 결과인 배열을 구분자를 사용하여 단어로만 이루어진 문자열로 만들어 유니티로 보낸다.
  * 유니티에서는 문자열을 `SIML`에서 확인하여 출력값을 만든다

## 실행방법

* `Server_text_tokenize.py`를 Python을 통해 실행 시켜준다

  ```text
  > python "Server_text_tokenize.py"
  ```

* 실행을 위해선 다음에 설명할 KoNLPy에 대한 환경설정이 필요하다.

# KoNLPy

## 환경설정

`KoNLPy`의 사용은 환경 구성과의 싸움이다. 정말 아무것도 모를수 있는 상태를 기반으로 하여 작성 되었다.

### colab

우월한 `colab`은

```python
pip install KoNLPy
```

한줄만으로 `KoNLPy` 사용에 모든 적합한 환경을 구성해 주신다.

만약 `colab`께서 특정 모듈이 없다고 한다면 Error을 잘 읽어보고 필요한 모듈을 추가로 `pip`를 통해 설치해 주도록 하자.

### Local Setting

축하한다. `colab`에서는 Local과 서버 통신을 위한 세팅이 불가 하므로 이 글을 읽고 있는 당신은 로컬 환경에서 어떻게든 세팅하기 위해 이 글을 읽고 있을 것이다.

우선, Local에서 설치하거나 실행하기 불가능 한 상황이라면 `GoormIDE`나 `Amazon Web Service`를 통하여 원격지에서 환경을 구성할 수 도 있을 것이다. 그러나 포트포워딩과 원격지 접속을 위한 세팅을 위해 이 프로젝트를 수정하기 위해서는 당신은 머리를 한참 긁적여야 할것이다. 이에 따른 설명은 이 글에서는 진행하지 않는다.



#### Enviroment Setting

다음 글은 `KoNLPy`의 정식 docs를 참고하여 작성되었음을 알린다.

KoNLPy Officail docs : [https://KoNLPy.org/ko/latest/install/](https://KoNLPy.org/ko/latest/install/)



##### Python

이 글에서는 `Python 3.7.x`버전을 지향한다.

이 프로젝트는 `python 3.7.8`버전을 기반으로 하여 개발 되었다.

* 만약 당신이 `Visual Studio`도 설치 하지 않고 `Anaconda`도 설치 하지 않고 컴퓨터가 순백의 상태라면 다음의 링크를 타고 들어가 `python 3.X.X`버전을 설치 해 주도록 하자.
  * python Install : [https://www.python.org/downloads/](https://www.python.org/downloads/)
    * `Looking for a specific release?` 의 릴리즈 버전에서 `python 3.7.8`버전을 찾아 링크를 타고 들어간다.
    * 자신의 PC기종과 버전에 맞는 것을 선택하여 설치 한다.
      * 윈도우 10의 경우 시작 > ''내 PC'' 검색 > 속성 에서 확인 가능하다.
      * 64bit인지 32bit인지 확인하여 설치 하도록 한다. (link : 3.7.8)
        * 64bit -> [Windows x86-64 executable installer](https://www.python.org/ftp/python/3.7.8/python-3.7.8-amd64.exe)
        * 32bit -> [Windows x86 executable installer](https://www.python.org/ftp/python/3.7.8/python-3.7.8.exe)

* 만약 당신이 개발해본 경험이 있다면, 높은 확률로 `Visual Studio`, `Anaconda`가 설치 되어 있을 것이다. 

  ```text
  >python
  >>>import sys
  >>>sys.executable
  'D:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\Python37_64\\python.exe'
  ```

  으로 cmd에 환경변수로 등록되어있는 python의 버전과 경로를 확인을 해주도록 하자. (환경변수 등록 및 수정방법은 이후 JAVA에서 기술한다.)

  작성자는 HDD 관리를 위해 `Visual Studio`를 `D:\\`에 설치한 결과 메인으로 실행되는 python의 위치를 찾지 못해 수많은 시간을 버렸다.

  * `python`을 여러 버전 설치하였다면, 로컬에서 해당 프로젝트 실행을 위해선 프로젝트를 위한 `python`의 위치로 PATH를 바꾸어 주도록 하자.

* `python`버전 확인 방법은 다음과 같다

  ```text
  >python --version
  Python 3.7.8
  ```

* `python 4.0.0`이상의 버전에서는 이후 기술할 몇몇 부분에서 실행이 안되므로  `python 3.X.X`버전을 설치 해 주도록 하자.

  * 이후 후술할 `JPYpe`의 최적화가 작성자 확인결과`python 3.7.X`버전에서 가장 뛰어나다
  * 또한 `tweepy` 모듈의 `StreamListener`은 `tweepy 4.0.0`이후 버전에서는 삭제되므로 `tweepy 3.10.0`에 가장 적합하도록 위의 파이썬 버전을 설치 하도록 한다.

##### JAVA

* `JDK version 1.7.X`이상의 java가 설치되어 있어야 한다.

* 시스템 환경변수에 `JAVA_HOME`이 설정 되어있어야 한다.

  * cmd창에서 다음과 같이 `java -version` 입력시 결과가 나오지 않는다면, 사용자 등록변수와 시스템 환경변수에 모두 `JAVA_HOME`이 설정되어 있는지 확인해 보자

  * ```TEXT
    > java -version
    openjdk version "1.8.0_302"
    OpenJDK Runtime Environment (Temurin)(build 1.8.0_302-b08)
    OpenJDK 64-Bit Server VM (Temurin)(build 25.302-b08, mixed mode)
    ```

  * 만약 등록 되어 있지 않다면,  윈도우 10 기준

    * 시작 > ''시스템 환경 변수 편집'' 검색
    * 고급 > "환경 변수" 탭 클릭
    * "~에 대한 사용자변수"의 "새로 만들기" 탭 클릭
      * 변수 이름 : JAVA_HOME
      * 변수 값 : C\Program Files\Java\jdk1.7.X~\jre\bin
        * 변수 값은 자신이 `JAVA` 설치한 위치에 따라 달라 지므로 자신이 `JDK`을 설치한 위치를 잘 알아 둘것. 자신이 정말 못 찾겠다면 프로그램 설치 제거를 통해 지웠다가 다시 깔아보는 절차를 밟아보자.
    * 확인 누르고 아래 "시스템 변수"에서도 위와 똑같이 진행

  * 위와 같은 세팅 이후에는 꼭 cmd창을 종료후 다시 실행하여 `JAVA`버전 확인을 진행 할 것

##### KoNLPy

우선 가장 필수 적인 2가지를 설치하여 진행 하였다. 이후 KoNLPy를 설치 하고 끝나면 좋으나, KoNLPy를 설치하는 과정에서 문제가 발생한다.

```text
pip install konlpy
```

위와 같이 pip를 통해 설치하여 무사히 끝난다면 당신은 행운아 이다. 다음과 같은 과정을 통해서 `KoNLPy`가 무사히 설치 되었는지 확인해 보자.

```text
>python

>>>from konlpy.tag import Okt
```

Okt(Open Korean Text)는 트위터에서 만든 오픈소스 한국어 처리 모듈인 twitter-korean-text를 계승하여 개발중인 오픈소스이다.

위와 같은 방법으로 설치를 진행하였다면, 

```text
>>> AttributeError: module 'tweepy' has no attribute 'StreamListener'
```

라고 말하며 잔뜩 성을 낼것이다.



###### tweepy

`tweepy`의 docs에 들어가 확인해 보면,

tweepy Docs : [https://docs.tweepy.org/](https://docs.tweepy.org/en/stable/faq.html?highlight=StreamListener#where-did-streamlistener-go)

상단의 `python`에서 설명하였듯  `tweepy` 모듈의 `StreamListener`은 `tweepy 4.0.0`이후 버전에서는 `Stream`클래스에 통합되어 

 `StreamListener`을 사용하기 위해서는`tweepy 3.10.0`을 설치해줄 필요가 있다.

```text
pip install tweepy==3.10.0
```

를 통해 `tweepy`를 버전에 맞추어 설치 해주도록 한다.

* 혹여나, 설치 과정중 문제가 발생한다면

```
pip install tweepy==3.10.0 --force-reinstall
```

을 통해 재설치를 진행한다.

```
pip install tweepy==3.10.0 --force-reinstall 	# 삭제후 설치
pip install tweepy==3.10.0 --ignore-installed 	# 덮어쓰기
```

위 둘의 차이는 `--force-reinstall`은 해당 라이브러리를 삭제후 재설치 한다는 것과 `--ignore-installed`는 현재 라이브러리를 삭제하지 않고 덮어쓰기 한다는 것이다. 그러니 꼭 `--force-reinstall`를 사용해 주도록 하자.



###### JPype

tweepy를 문제 해결하더라도 다음과 같이 문제가 발생 할수 있다. 

```text
SystemError: java.nio.file.InvalidPathException: Illegal char <*> at index 3: C:\ ~~~~ \Lib\site-packages\konlpy\java\*
```

* 우선 `JPype`는 `Java`에서 작성한 라이브러리를 사용 할수 있도록 도와주는 라이브러리이다. Okt()의 경우 `Java`로 작성되었기 때문에 이와 같은 문제가 발생 하는 것으로 보인다.

* 이에따라 다음 순서를 따라 `JPype`를 `Python`버전에 맞게 설치해 주도록 하자.

  JPype install link : [JPype](https://www.lfd.uci.edu/~gohlke/pythonlibs/#jpype)

 ```
 JPype1‑1.3.0‑cp37‑cp37m‑win_amd64.whl
 JPype1‑1.3.0‑cp37‑cp37m‑win32.whl
 ```

위와 같은 식으로 파일들이 있다면

```
JPype1‑1.3.0(version)‑cp37(python_version)‑cp37m‑win32.whl
```

이므로 `python(37==3.7.X)`버전과 pc환경설정에 맞게 잘 설치 하도록 하자.



* 설치 방법

```
> pip install JPype1‑1.3.0‑cp37‑cp37m‑win32.whl --force--reinstall
```



# Python과 C#, 그리고 KoNLPy 마무리

위의 방법을 따라서 고생고생해서 설치를 완료했다면 축하한다. 드디어 이 프로젝트를 실행할 준비가 거의 완료 되었다.

