using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Net;
using Syn.Bot.Siml;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

public class Message
{
    public string Text;
    public Text TextObject;
    public MessageType MessageType;
   
}

public enum MessageType
{
    User, Bot

}

//public class BotDialog : Dialog
//{
//  [Expression("Hello Bot")]
//public void Hello(Context context, Result result)
//{
//   result.SendResponse("Hello User!");
//}


//}


public class ChatManager : MonoBehaviour
{
    //OscovaBot MainBot;
    SimlBot Chatbot;
    TextToSpeech TTS;
    LipSyncManager LSM;
    Pronunciation_Kor_En PKE;

    List<Message> Messages = new List<Message>();

    public GameObject chatPanel, textObject;
    public InputField chatBox;
    public Color UserColor, BotColor;

    void Start()
    {
        try
        {
            //MainBot = new OscovaBot();
            //OscovaBot.Logger.LogReceived += (s, o) =>
            //{
            //  Debug.Log($"OscovaBot: {o.Log}");
            // };

            //MainBot.Dialogs.Add(new BotDialog());
            //MainBot.Trainer.StartTraining();

            //MainBot.MainUser.ResponseReceived += (sender, evt) =>
            //{
            //  AddMessage($"Bot: {evt.Response.Text}",MessageType.Bot);
            //};
            Chatbot = new SimlBot();
            Chatbot.PackageManager.LoadFromString(File.ReadAllText("Hello_Bot.simlpk"));

            SimlBot.Logger.LogReceived += (s, o) =>
            {
                Debug.Log("$SimlBot: {o.Log}");
            };

            TTS = GetComponent<TextToSpeech>();
            LSM = this.transform.Find("Head").gameObject.GetComponent<LipSyncManager>();
            PKE = GetComponent<Pronunciation_Kor_En>();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

    }
    //void Update()
    //{

    //}
    public void AddMessage(string messageText, MessageType messageType)
    {
        if (Messages.Count >= 25)
        {
            Destroy(Messages[0].TextObject.gameObject);
            Messages.Remove(Messages[0]);

        }
        var newMessage = new Message { Text = messageText };

        var newText = Instantiate(textObject, chatPanel.transform);

        newMessage.TextObject = newText.GetComponent<Text>();
        newMessage.TextObject.text = messageText;
        newMessage.TextObject.color = messageType == MessageType.User ? UserColor : BotColor;

        Messages.Add(newMessage);


    }
    public void SendMessageToBot()
    {
        var userMessage = chatBox.text;
        var printMessage = token(userMessage);

        if (!string.IsNullOrEmpty(userMessage))
        {
            Debug.Log($"SimlBot:[USER] {userMessage}");
            AddMessage($"User: {userMessage}", MessageType.User);
            //var request = MainBot.MainUser.CreateRequest(userMessage);
            //var evaluationResult = MainBot.Evaluate(request);
            //evaluationResult.Invoke();

            var result = Chatbot.Chat(printMessage);
            Debug.Log(result.BotMessage);
            string res_str = PKE.InputString(result.BotMessage);

            LSM.Input = res_str;
            LSM.PutInputIntoQueue = true;
            TTS.Init(result.BotMessage);


            AddMessage($"Bot: {result.BotMessage}", MessageType.Bot);

            chatBox.Select();
            chatBox.text = "";

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageToBot();
        }

    }

    public string token(string message)
    {
        using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            // Connect 함수로 로컬(127.0.0.1)의 포트 번호 9999로 대기 중인 socket에 접속한다.
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999));
            // 보낼 메시지를 UTF8타입의 byte 배열로 변환한다.
            var data = Encoding.UTF8.GetBytes(message);
            // big엔디언으로 데이터 길이를 변환하고 서버로 보낼 데이터의 길이를 보낸다. (4byte)
            client.Send(BitConverter.GetBytes(data.Length));
            // 데이터를 전송한다.
            client.Send(data);
            // 데이터의 길이를 수신하기 위한 배열을 생성한다. (4byte)
            data = new byte[4];
            // 데이터의 길이를 수신한다.
            client.Receive(data, data.Length, SocketFlags.None);
            // server에서 big엔디언으로 전송을 했는데도 little 엔디언으로 온다. big엔디언과 little엔디언은 배열의 순서가 반대이므로 reverse한다.
            Array.Reverse(data);
            // 데이터의 길이만큼 byte 배열을 생성한다.
            data = new byte[BitConverter.ToInt32(data, 0)];
            // 데이터를 수신한다.
            client.Receive(data, data.Length, SocketFlags.None);
            // 수신된 데이터를 UTF8인코딩으로 string 타입으로 변환 후에 콘솔에 출력한다.
            Console.WriteLine(Encoding.UTF8.GetString(data));

            return Encoding.UTF8.GetString(data);
        }
    }
}


