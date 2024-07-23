using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContentManager : MonoBehaviour
{
    //物件取用
    [SerializeField] public GameObject GO_BackgroundImg;
    [SerializeField] public GameObject GO_FaceImg;
    [SerializeField] public GameObject GO_NameText;
    [SerializeField] public GameObject GO_ContentText;
    [SerializeField] public GameObject GO_CenterText;
    [SerializeField] StoryResourceLoad storyResourceLoad;

    //使用故事UI的策略
    StoryUITextController _storyUITextController;

    int _StoryChapter = 0;
    int _StoryProcess = 0;

    //指令(普通對話=$0|發出音效=$1|改變背景音樂=$2|改變背景=$3|使用中間的對話框=$4|離開故事(更換場景)=$5),指令附帶參數,頭像($0=淨空),名字($0=淨空),對話內容($0=淨空)
    List<string> List_command = new List<string>();
    //背景|(音樂)|(音效)
    List<string> List_refVariable = new List<string>();
    List<string> List_faceImg = new List<string>();
    List<string> List_characterName = new List<string>();
    List<string> List_dialog = new List<string>();

    public void SetChapter(int receiveChapter)
    {
        _StoryChapter = receiveChapter;
        TextAsset textFile = Resources.Load<TextAsset>("Story/Story_" + _StoryChapter.ToString());
        string textFile_content = textFile.text;
        string[] lines = textFile_content.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            string[] lineCommands = lines[i].Split(',');
            if (lineCommands.Length < 5) return;
            List_command.Add(lineCommands[0]);
            List_refVariable.Add(lineCommands[1]);
            List_faceImg.Add(lineCommands[2]);
            List_characterName.Add(lineCommands[3]);
            List_dialog.Add(lineCommands[4]);
        }
    }

    public void ShowStory()
    {
        string getIndividualCommand = List_command[_StoryProcess];

        string[] getRefVariable = List_refVariable[_StoryProcess].Split('|');
        switch (getIndividualCommand)
        {
            case "$0":
                SetStoryUITextController(new StoryUITextController_Strategy_Normal(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText));
                ShowCenterStoryUI(getRefVariable[0]);
                break;
            case "$0$2$3":
                SetStoryUITextController(new StoryUITextController_Strategy_Normal(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText));
                SetBGM(getRefVariable[2]);
                PlaySound(getRefVariable[1]);
                ShowCenterStoryUI(getRefVariable[0]);
                break;
            case "$1":
                SetStoryUITextController(new StoryUITextController_Strategy_Center(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText));
                ShowCenterStoryUI(getRefVariable[0]);
                break;
            case "$1$3": //使用中間對話框、改變背景音樂
                //設定背景音樂
                SetStoryUITextController(new StoryUITextController_Strategy_Center(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText));
                SetBGM(getRefVariable[1]);
                ShowCenterStoryUI(getRefVariable[0]);
                break;
            case "$4":
                GameManagerSingleton.Instance.SceneStateController.SceneStateUpdate();
                GameManagerSingleton.Instance.BGM_Manager.Stop();
                GameManagerSingleton.Instance.GameProcess.StoryProcess = _StoryProcess;
                break;
            default:
                Debug.LogError("something wrong!");
                break;
        }
    }

    private void SetStoryUITextController(StoryUITextController storyUITextController)
    {
        _storyUITextController = storyUITextController;
    }

    private void SetBGM(string getRefVariable_bgmAudio)
    {
        //Debug.Log(getRefVariable_bgmAudio);
        if (!(getRefVariable_bgmAudio.CompareTo(GameManagerSingleton.Instance.BGM_Manager.clip.name) == 0))
        {
            //Debug.Log(getRefVariable_bgmAudio);
            //Debug.Log(GameManagerSingleton.Instance.BGM_Manager.clip.name);
            GameManagerSingleton.Instance.BGM_Manager.clip = searchAudioClip(storyResourceLoad.ME, getRefVariable_bgmAudio);
            GameManagerSingleton.Instance.BGM_Manager.loop = true;
            GameManagerSingleton.Instance.BGM_Manager.Play();
        }
    }

    private void PlaySound(string getRefVariable_SoundAudio)
    {
        SoundEffectSingleton.Instance.Sound_Manager.clip = searchAudioClip(storyResourceLoad.SE, getRefVariable_SoundAudio);
        SoundEffectSingleton.Instance.Sound_Manager.Play();
    }
    
    private AudioClip searchAudioClip(List<AudioClip> audioClips, string audioClipName)
    {
        foreach(AudioClip audioClip in audioClips)
        {
            if(audioClipName.CompareTo(audioClip.name) == 0)
            {
                return audioClip;
            }
        }
        return null;
    }

    private void ShowCenterStoryUI(string getRefVariable_bkImg)
    {
        //設定StoryUITextController操作介面
        Sprite getBackGroundImg = searchSprite(storyResourceLoad.Img_BackGound, getRefVariable_bkImg);
        Sprite getHeadImg = searchSprite(storyResourceLoad.Img_Face, List_faceImg[_StoryProcess]);
        string getCharacterName = "";
        if (List_characterName[_StoryProcess].CompareTo("$0") != 0)
        {
            getCharacterName = List_characterName[_StoryProcess];
        }
        string getDialog = "";
        if (List_dialog[_StoryProcess].CompareTo("$0") != 0)
        {
            getDialog = List_dialog[_StoryProcess].Replace("\\n","\n");
        }
        //使用Story
        _storyUITextController.Show(getBackGroundImg, getHeadImg, getCharacterName, getDialog);
    }

    private Sprite searchSprite(List<Sprite> sprites,string spriteName)
    {
        foreach(Sprite sprite in sprites)
        {
            if(spriteName.CompareTo(sprite.name) == 0)
            {
                return sprite;
            }
        }
        return null;
    }

    public void SetProcess(int receiveProcess)
    {
        _StoryProcess = receiveProcess;
        ShowStory();
    }

    public void nextProcess()
    {
        _StoryProcess += 1;
        if (_StoryProcess > List_command.Count-1)
        {
            _StoryProcess = List_command.Count-1;
        }
        ShowStory();
    }

    public void lastProcess()
    {
        _StoryProcess -= 1;
        if (_StoryProcess < 0)
        {
            _StoryProcess = 0;
        }
        ShowStory();
    }


}