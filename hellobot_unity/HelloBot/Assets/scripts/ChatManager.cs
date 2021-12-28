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

        if (!string.IsNullOrEmpty(userMessage))
        {
            Debug.Log($"SIMLBot:[USER] {userMessage}");
            AddMessage($"User: {userMessage}", MessageType.User);
            //var request = MainBot.MainUser.CreateRequest(userMessage);
            //var evaluationResult = MainBot.Evaluate(request);
            //evaluationResult.Invoke();

            var result = Chatbot.Chat(userMessage);
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
}


