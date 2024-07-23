using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI_Name
{
    Close,
    Feed,
    System
}

public class HomeSceneButton : MonoBehaviour
{
    IAssetFactory assetFactory;
    IButtonFunction feedBtnFunction;
    IButtonFunction systemBtnFunction;
    IButtonFunction foodBtnFunction;
    IButtonFunction canBtnFunction;
    IButtonFunction saveBtnFunction;
    IButtonFunction returnTitleFunction;
    IButtonFunction closeBtnFunction;

    [SerializeField] Button feedBtn;
    [SerializeField] Button systemBtn;

    public static GameObject Function_UI;
    public static GameObject FeedFunction_UI;
    public static GameObject SystemFunction_UI;
    public static GameObject Saved_Text;

    public static int functionUI_CurrentDisplay = (int) UI_Name.Close; //0為關閉狀態、1為feed、2為system
    public UI_Name functionUI_ButtonClick = (int) UI_Name.Close;

    public static bool bHomeUIUse = true;

    public bool bIsInAnimation;

    private void Awake()
    {
        Function_UI = GameObject.Find("Function_UI");
        FeedFunction_UI = Function_UI.transform.Find("MainPanel").transform.Find("Feed").gameObject;
        SystemFunction_UI = Function_UI.transform.Find("MainPanel").transform.Find("System").gameObject;
        Saved_Text = Function_UI.transform.Find("Btn_Group").transform.Find("Save_Text").gameObject;

        assetFactory = new ResourceAssetProxyFactory();
        feedBtnFunction = new FeedBtnFunction();
        systemBtnFunction = new SystemBtnFunction();
        foodBtnFunction = new FoodBtnFunction();
        canBtnFunction = new CanBtnFunction();
        saveBtnFunction = new SaveBtnFunction();
        returnTitleFunction = new ReturnTitleFunction();
        closeBtnFunction = new CloseBtnFunction();
    }

    IEnumerator EnableButton(Button button)
    {
        yield return new WaitForSeconds(1f);
        if (bHomeUIUse)
        {
            button.interactable = true;
        }
    }

    public void FeedBtnClick()
    {
        feedBtn.interactable = false;
        feedBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));
        StartCoroutine(EnableButton(feedBtn));
    }

    public void SystemBtnClick()
    {
        systemBtn.interactable = false;
        systemBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));
        StartCoroutine(EnableButton(systemBtn));
    }

    public void FoodBtnClick()
    {

        foodBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));

    }

    public void CanBtnClick()
    {

        canBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));

    }

    public void SaveBtnClick()
    {

        saveBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));
    }

    public void ReturnTitleBtnClick()
    {
        returnTitleFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));
        functionUI_CurrentDisplay = (int) UI_Name.Close;
    }

    public void CloseGameBtnClick()
    {
        closeBtnFunction.DoEffect(assetFactory.LoadSoundAudio("Decision2"));
    }

    public static void UI_Control_Method(UI_Name uiName)
    {
        if (functionUI_CurrentDisplay == (int)UI_Name.Close || functionUI_CurrentDisplay == (int)uiName)
        {
            if (functionUI_CurrentDisplay == (int)uiName)
            {
                functionUI_CurrentDisplay = (int)UI_Name.Close;
                Function_UI.GetComponent<Animator>().SetTrigger("Slide");
                return;
            }
            Function_UI.GetComponent<Animator>().SetTrigger("Slide");
        }
        functionUI_CurrentDisplay = (int)uiName;
    }


    public static void HomeUIUse(bool state)
    {
        bHomeUIUse = state;
        Function_UI.transform.Find("Btn_Group").transform.Find("Feed_Btn").gameObject.GetComponent<Button>().interactable = state;
        Function_UI.transform.Find("Btn_Group").transform.Find("System_Btn").gameObject.GetComponent<Button>().interactable = state;
    }

    float saveTextCloseCount;
    private void Update()
    {
        //if (!bHomeUIUse)
        //{
        //    Function_UI.transform.Find("Btn_Group").transform.Find("Feed_Btn").gameObject.GetComponent<Button>().interactable = false;
        //    Function_UI.transform.Find("Btn_Group").transform.Find("System_Btn").gameObject.GetComponent<Button>().interactable = false;
        //}
        //else
        //{
        //    Function_UI.transform.Find("Btn_Group").transform.Find("Feed_Btn").GetComponent<Button>().interactable = true;
        //    Function_UI.transform.Find("Btn_Group").transform.Find("System_Btn").GetComponent<Button>().interactable = true;
        //}

        if (Saved_Text.activeSelf)
        {
            saveTextCloseCount += Time.deltaTime;
            if (saveTextCloseCount > 2)
            {
                Saved_Text.SetActive(false); 
                saveTextCloseCount = 0;
            }
        }
    }
}

public class FeedBtnFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {

        //if (HomeSceneButton.functionUI_CurrentDisplay == (int) UI_Name.Close || HomeSceneButton.functionUI_CurrentDisplay == (int) UI_Name.Feed)
        //{
        //    if(HomeSceneButton.functionUI_CurrentDisplay == (int)UI_Name.Feed)
        //    {
        //        HomeSceneButton.functionUI_CurrentDisplay = (int)UI_Name.Close;
        //        HomeSceneButton.Function_UI.GetComponent<Animator>().SetTrigger("Slide");
        //        return;
        //    }
        //    HomeSceneButton.Function_UI.GetComponent<Animator>().SetTrigger("Slide");
        //}
        //HomeSceneButton.functionUI_CurrentDisplay = (int)UI_Name.Feed;
        HomeSceneButton.UI_Control_Method(UI_Name.Feed);

        HomeSceneButton.FeedFunction_UI.gameObject.SetActive(true);
        HomeSceneButton.SystemFunction_UI.gameObject.SetActive(false);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class SystemBtnFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {

        //if (HomeSceneButton.functionUI_CurrentDisplay == (int) UI_Name.Close || HomeSceneButton.functionUI_CurrentDisplay == (int) UI_Name.System)
        //{
        //    if(HomeSceneButton.functionUI_CurrentDisplay == (int)UI_Name.System)
        //    {
        //        HomeSceneButton.functionUI_CurrentDisplay = (int)UI_Name.Close;
        //        HomeSceneButton.Function_UI.GetComponent<Animator>().SetTrigger("Slide");
        //        return;
        //    }
        //    HomeSceneButton.Function_UI.GetComponent<Animator>().SetTrigger("Slide");
        //}
        //HomeSceneButton.functionUI_CurrentDisplay = (int)UI_Name.System;
        HomeSceneButton.UI_Control_Method(UI_Name.System);

        HomeSceneButton.FeedFunction_UI.gameObject.SetActive(false);
        HomeSceneButton.SystemFunction_UI.gameObject.SetActive(true);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class FoodBtnFunction : IButtonFunction
{
    IAssetFactory assetFactory = new ResourceAssetProxyFactory();

    protected override void ButtonMainFunction()
    {
        HomeSceneButton.UI_Control_Method(UI_Name.Feed);
        GameObject go = assetFactory.LoadFoodObj("foodObj");
        HomeSceneButton.HomeUIUse(false);
        HomeSceneButton.FeedFunction_UI.gameObject.SetActive(true);
        HomeSceneButton.SystemFunction_UI.gameObject.SetActive(false);
        HomeManager.CreateHomeSceneObject(go);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class CanBtnFunction : IButtonFunction
{
    IAssetFactory assetFactory = new ResourceAssetProxyFactory();
    protected override void ButtonMainFunction()
    {
        HomeSceneButton.UI_Control_Method(UI_Name.Feed);
        GameObject go = assetFactory.LoadFoodObj("canObj");
        HomeSceneButton.HomeUIUse(false);
        HomeSceneButton.FeedFunction_UI.gameObject.SetActive(true);
        HomeSceneButton.SystemFunction_UI.gameObject.SetActive(false);
        HomeManager.CreateHomeSceneObject(go);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class CloseBtnFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {
        Debug.Log("ys");
        Application.Quit();
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class ReturnTitleFunction : IButtonFunction
{
    SceneStateController sceneStateController = GameManagerSingleton.Instance.SceneStateController;
    AudioSource bgmController = GameManagerSingleton.Instance.BGM_Manager;

    protected override void ButtonMainFunction()
    {
        bgmController.Stop();
        sceneStateController.SetSceneState(new TitleSceneState(sceneStateController), "TitleScene");

    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

public class SaveBtnFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {
        SaveAndLoadSystem saveAndLoadSystem = new SaveAndLoadSystem();
        saveAndLoadSystem.Save();
        HomeSceneButton.Saved_Text.SetActive(true);


        HomeSceneButton.UI_Control_Method(UI_Name.System);

        HomeSceneButton.FeedFunction_UI.gameObject.SetActive(false);
        HomeSceneButton.SystemFunction_UI.gameObject.SetActive(true);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}