using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HomeLoveUIController : MonoBehaviour
{
    [SerializeField] GameObject Face_Cat;
    [SerializeField] GameObject Face_Fox;
    [SerializeField] GameObject Love_Slide_Cat;
    [SerializeField] GameObject Love_Slide_Fox;
    [SerializeField] GameObject Communicate_Cat;
    [SerializeField] GameObject Communicate_Fox;

    [SerializeField] List<Sprite> CharacterFaces = new List<Sprite>();

    [SerializeField] public GameObject Unlock_R18_Btn;
    [SerializeField] public GameObject NoShit_UI;

    public HomeSceneDialogReader Cat_Dialog;
    public HomeSceneDialogReader Fox_Dialog;

    private IAssetFactory assetFactory;
    private IButtonFunction R18_btn_function;

    private bool bShowDialog = false;

    private float DialogCloseLimitTime = 7f;
    private float DialogCloseTimeCount = 0;

    private AudioClip currentAudioClip;

    GameProcess gameProcess;

    private void Awake()
    {
        gameProcess = GameManagerSingleton.Instance.GameProcess;
        TextAsset textFile = Resources.Load<TextAsset>("Story/HomeDialog_Fox");
        Fox_Dialog = new HomeSceneDialogReader(textFile);
        textFile = Resources.Load<TextAsset>("Story/HomeDialog_Cat");
        Cat_Dialog = new HomeSceneDialogReader(textFile);
        LoadCharacterFaces();
        HomeManager.SetHomeLoveUIContorller(this);
    }

    void Start()
    {
        assetFactory = new ResourceAssetProxyFactory();
        Love_Slide_Cat.GetComponent<Slider>().value = gameProcess.CatLove;
        Love_Slide_Fox.GetComponent<Slider>().value = gameProcess.FoxLove;
    }


    void LoadCharacterFaces()
    {
        SpriteLoad(CharacterFaces, "CharacterFace/Cat1");
        SpriteLoad(CharacterFaces, "CharacterFace/Cat2");
        SpriteLoad(CharacterFaces, "CharacterFace/Cat3");
        SpriteLoad(CharacterFaces, "CharacterFace/Fox1");
        SpriteLoad(CharacterFaces, "CharacterFace/Fox2");
        SpriteLoad(CharacterFaces, "CharacterFace/Fox3");
        SpriteLoad(CharacterFaces, "CharacterFace/Fox4");
    }

    private void SpriteLoad(List<Sprite> spriteList, string route)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(route);
        foreach (Sprite picture in sprites)
        {
            spriteList.Add(picture);
        }
    }

    private Sprite GetSprite(string spriteName)
    {
        foreach(Sprite sprite in CharacterFaces)
        {
            if (sprite.name.CompareTo(spriteName) == 0)
            {
                return sprite;
            }
        }
        return null;
    }

    public void UpdateContent(HomeDialog CatContent, HomeDialog FoxContent)
    {
        ResetTimeLimit();

        Face_Cat.GetComponent<Image>().sprite = GetSprite(CatContent.sprite);
        Face_Fox.GetComponent<Image>().sprite = GetSprite(FoxContent.sprite);
        Communicate_Cat.transform.GetChild(0).GetComponent<Text>().text = CatContent.dialog;
        Communicate_Fox.transform.GetChild(0).GetComponent<Text>().text = FoxContent.dialog;
        Love_Slide_Cat.GetComponent<Slider>().value = gameProcess.CatLove;
        Love_Slide_Fox.GetComponent<Slider>().value = gameProcess.FoxLove;
    }

    internal void UpdateContent(List<HomeDialog> Dialog, string characterName)
    {
        ResetTimeLimit();
        HomeDialog temp;
        temp = Dialog[Random.Range(0, Dialog.Count)];
        if(characterName == "CatWalk")
        {
            Face_Cat.GetComponent<Image>().sprite = GetSprite(temp.sprite);
            Communicate_Cat.transform.GetChild(0).GetComponent<Text>().text = temp.dialog;
            Love_Slide_Cat.GetComponent<Slider>().value = gameProcess.CatLove;
        }
        if(characterName == "FoxWalk")
        {
            Face_Fox.GetComponent<Image>().sprite = GetSprite(temp.sprite);
            Communicate_Fox.transform.GetChild(0).GetComponent<Text>().text = temp.dialog;
            Love_Slide_Fox.GetComponent<Slider>().value = gameProcess.FoxLove;
        }
    }

    private void ResetTimeLimit()
    {
        DialogCloseTimeCount = 0;
    }

    public void UpdateContent(List<HomeDialog> CatContent, List<HomeDialog> FoxContent)
    {
        HomeDialog temp_Cat;
        HomeDialog temp_Fox;
        int cat_Say, fox_Say;
        cat_Say = Random.Range(0, CatContent.Count);
        fox_Say = Random.Range(0, FoxContent.Count);
        temp_Cat = CatContent[cat_Say];
        temp_Fox = FoxContent[fox_Say];

        UpdateContent(temp_Cat, temp_Fox);
    }

    public void SetDialogCloseLimitTime(float theDialogCloseLimitTime)
    {
        DialogCloseLimitTime = theDialogCloseLimitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (bShowDialog)
        {
            DialogCloseTimeCount += Time.deltaTime;
        }
        if (DialogCloseTimeCount > DialogCloseLimitTime)
        {
            bShowDialog = false;
            SetShowDialog(bShowDialog);
            DialogCloseTimeCount = 0;
            UpdateContent(Cat_Dialog.Default_Dialog, Fox_Dialog.Default_Dialog);
        }

        //Debug.Log(NoShit_UI.transform.GetChild(0).transform.Find("NoShit_Player").GetComponent<VideoPlayer>().isPlaying);
    }

    bool bIsPlayR18;
    float timeCount;

    private void LateUpdate()
    {
        if (bIsPlayR18)
        {
            timeCount += Time.deltaTime;
            if (timeCount > NoShit_UI.transform.GetChild(0).transform.Find("NoShit_Player").GetComponent<VideoPlayer>().length+0.5f)
            {
                bIsPlayR18 = false;
                NoShit_UI.SetActive(false);
                GameManagerSingleton.Instance.BGM_Manager.clip = currentAudioClip;
                GameManagerSingleton.Instance.BGM_Manager.Play();
                timeCount = 0;
            }
        }
    }


    public void SetShowDialog(bool bShow)
    {
        bShowDialog = bShow;
        Communicate_Cat.SetActive(bShow);
        Communicate_Fox.SetActive(bShow);
    }

    public void SetShowDialog(bool bShow, string characterName)
    {
        bShowDialog = bShow;
        if (characterName == "CatWalk") Communicate_Cat.SetActive(bShow);
        if (characterName == "FoxWalk") Communicate_Fox.SetActive(bShow);
    }

    public void SetUnlockR18Btn(bool bActive)
    {
        Unlock_R18_Btn.SetActive(bActive);
    }

    public void UnlockR18Btn()
    {
        SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(assetFactory.LoadSoundAudio("Decision2"));
        currentAudioClip = GameManagerSingleton.Instance.BGM_Manager.clip;
        GameManagerSingleton.Instance.BGM_Manager.Stop();
        bIsPlayR18 = true;
        NoShit_UI.SetActive(true);
        NoShit_UI.transform.GetChild(0).transform.Find("NoShit_Player").GetComponent<VideoPlayer>().Play();
    }
}