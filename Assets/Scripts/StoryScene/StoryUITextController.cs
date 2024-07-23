using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StoryUITextController
{
    //物件取用
    public GameObject _GO_BackgroundImg;
    public GameObject _GO_FaceImg;
    public GameObject _GO_NameText;
    public GameObject _GO_ContentText;
    public GameObject _GO_CenterText;

    public StoryUITextController(GameObject GO_BackgroundImg, GameObject GO_FaceImg, GameObject GO_NameText, GameObject GO_ContentText, GameObject GO_CenterText)
    {
        _GO_BackgroundImg = GO_BackgroundImg;
        _GO_FaceImg = GO_FaceImg;
        _GO_NameText = GO_NameText;
        _GO_ContentText = GO_ContentText;
        _GO_CenterText = GO_CenterText;
    }

    public abstract void Show(Sprite BackGroundImg,Sprite FaceImg, string NameText, string contentText);
}

public class StoryUITextController_Strategy_Normal : StoryUITextController
{
    public StoryUITextController_Strategy_Normal(GameObject GO_BackgroundImg, GameObject GO_FaceImg, GameObject GO_NameText, GameObject GO_ContentText, GameObject GO_CenterText):base(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText)
    {
    }

    public override void Show(Sprite BackGroundImg, Sprite FaceImg, string NameText, string contentText)
    {
        _GO_BackgroundImg.GetComponent<Image>().sprite = BackGroundImg;
        _GO_FaceImg.GetComponent<Image>().sprite = FaceImg;
        _GO_NameText.GetComponent<Text>().text = NameText;
        _GO_ContentText.GetComponent<Text>().text = contentText;
        _GO_CenterText.GetComponent<Text>().text = "";
    }
}

public class StoryUITextController_Strategy_Center : StoryUITextController
{
    public StoryUITextController_Strategy_Center(GameObject GO_BackgroundImg, GameObject GO_FaceImg, GameObject GO_NameText, GameObject GO_ContentText, GameObject GO_CenterText):base(GO_BackgroundImg, GO_FaceImg, GO_NameText, GO_ContentText, GO_CenterText)
    {
    }

    public override void Show(Sprite BackGroundImg, Sprite FaceImg, string NameText, string contentText)
    {
        _GO_BackgroundImg.GetComponent<Image>().sprite = BackGroundImg;
        _GO_FaceImg.GetComponent<Image>().sprite = FaceImg;
        _GO_NameText.GetComponent<Text>().text = "";
        _GO_ContentText.GetComponent<Text>().text = "";
        _GO_CenterText.GetComponent<Text>().text = contentText;
    }
}