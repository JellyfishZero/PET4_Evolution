using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneInit : MonoBehaviour
{
    [SerializeField] GameObject fadeInOutPrebab;
    GameObject fadeInOutManager;
    GameProcess gameProcess;

    [SerializeField]
    public StoryContentManager storyContentManager;

    bool testMode;

    private void Awake()
    {
        fadeInOutManager = GameObject.Find("UI_FadeInOut");
        if (fadeInOutManager == null) //開發測試模式。
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Music/maou_fantasy_13");
            GameManagerSingleton.Instance.BGM_Manager.clip = audioClip;
            GameManagerSingleton.Instance.BGM_Manager.volume = .2f;
            Instantiate(fadeInOutPrebab);
            fadeInOutManager = GameObject.Find("UI_FadeInOut(Clone)");
            GameManagerSingleton.Instance.SceneStateController.SetSceneState(new StorySceneState(GameManagerSingleton.Instance.SceneStateController), "");
            testMode = true;
        }
        gameProcess = GameManagerSingleton.Instance.GameProcess; //取得當前劇情進度

        //storyContentManager為操作故事模式的facade，他會去操作所有子系統，如音樂、音效、圖片、劇情、場景轉換。
        storyContentManager.SetChapter(gameProcess.StoryChapter); //設定章節
        storyContentManager.SetProcess(gameProcess.StoryProcess); //設定劇情進度
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(testMode);
        if (!testMode)
        {
            fadeInOutManager.GetComponent<UI_FadeInOutAndSceneManage>().ForceTrigger();
        }

    }
}
