using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

[System.Serializable]

public class SetTextToSpeech 
{
    public SetInput input;
    public SetVoice voice;
    public SetAudioConfig audioConfig;
}
[System.Serializable]
public class SetAudioConfig
{
    public string audioEncoding;
    public float speakingRate;
    public int pitch;
    public int volumeGainDb;

}
[System.Serializable]

public class SetVoice
{
    public string languageCode;
    public string name;
    public string ssmlGender;

}
[System.Serializable]

public class SetInput
{
    public string text;
}
[System.Serializable]
public class GetContent
{
    public string audioContent;

}
public class TextToSpeech : MonoBehaviour
{
    private string apiURL = "https://texttospeech.googleapis.com/v1beta1/text:synthesize?key=AIzaSyCfHNpqUW2R9hBLIJthqRXCE4OgLWL1bqY";
    SetTextToSpeech tts = new SetTextToSpeech();
    public AudioSource audioSource_sound = null;
    //public AudioSource audioSource_sync = null;


    void Start()
    {
        //Init();
        //CreateAudio();
        //audioSource_sync = this.transform.Find("LipSyncTextureFlipTarget_Robot").gameObject.GetComponent<AudioSource>();
    }

    //public string text
    public void Init(string text)
    {
        SetInput si = new SetInput();
        si.text = text;
        tts.input = si;

        SetVoice sv = new SetVoice();
        sv.languageCode = "ko-KR";
        sv.name = "ko-KR-Standard-A";
        sv.ssmlGender = "FEMALE";
        tts.voice = sv;

        SetAudioConfig sa = new SetAudioConfig();
        sa.audioEncoding = "LINEAR16";
        sa.speakingRate = 0.5f;
        sa.pitch = -6;
        sa.volumeGainDb = 0;
        tts.audioConfig = sa;

        CreateAudio();
    }
    private void CreateAudio()
    {
        var str = TextToSpeechPost(tts);
        GetContent info = JsonUtility.FromJson<GetContent>(str);

        var bytes = Convert.FromBase64String(info.audioContent);

        var f = ConvertByteToFloat(bytes);

        AudioClip audioClip = AudioClip.Create("audioContent", f.Length, 1, 44100, false);
        audioClip.SetData(f,0);
        
        //if((audioSource_sound != null) && (audioSource_sync != null))

        if(audioSource_sound != null)
        {
            audioSource_sound.PlayOneShot(audioClip);
            //audioSource_sync.PlayOneShot(audioClip);
        }
        //AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();
        //audioSource.PlayOneShot(audioClip);
    }

    private static float[] ConvertByteToFloat(byte[] array)
    {
        float[] floatArr = new float[array.Length / 2];

        for(int i=0; i<floatArr.Length; i++)
        {
            floatArr[i] = BitConverter.ToInt16(array, i * 2) / 32768.0f;
        }
        return floatArr;

    }
    public string TextToSpeechPost(object data)
    {
        string str = JsonUtility.ToJson(data);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;

        try
        {
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            return json;

        }catch(WebException e)
        {
            Debug.Log(e);
            return null;
        }
    }

    void Update()
    {


    }

}