using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneInit : MonoBehaviour
{
    [SerializeField] GameObject fadeInOutPrebab;
    GameObject fadeInOutManager;
    GameProcess gameProcess;
    public static IAssetFactory assetFactory;

    [SerializeField] GameObject CatWalk;
    [SerializeField] GameObject FoxWalk;

    bool testMode;

    private void Awake()
    {
        assetFactory = new ResourceAssetProxyFactory();
        fadeInOutManager = GameObject.Find("UI_FadeInOut");
        if (fadeInOutManager == null) //開發測試模式。
        {
            gameProcess = GameManagerSingleton.Instance.GameProcess; //取得當前劇情進度
            gameProcess.CatLove = 90;
            gameProcess.FoxLove = 90;
            Instantiate(fadeInOutPrebab);
            fadeInOutManager = GameObject.Find("UI_FadeInOut(Clone)");
            GameManagerSingleton.Instance.SceneStateController.SetSceneState(new HomeSceneState(GameManagerSingleton.Instance.SceneStateController), "");
            testMode = true;
        }
        GameManagerSingleton.Instance.BGM_Manager.clip = assetFactory.LoadMusicAudio("maou_fantasy_10");
        GameManagerSingleton.Instance.BGM_Manager.volume = .2f;
        GameManagerSingleton.Instance.BGM_Manager.Play();


        //storyContentManager為操作故事模式的facade，他會去操作所有子系統，如音樂、音效、圖片、劇情、場景轉換。
        //storyContentManager.SetChapter(gameProcess.StoryChapter); //設定章節
        //storyContentManager.SetProcess(gameProcess.StoryProcess); //設定劇情進度
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!testMode)
        {
            fadeInOutManager.GetComponent<UI_FadeInOutAndSceneManage>().ForceTrigger();
        }
    }

    private void Update()
    {
        if(CatWalk.transform.position.y > FoxWalk.transform.position.y)
        {
            CatWalk.GetComponent<SpriteRenderer>().sortingOrder = 1;
            FoxWalk.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else
        {
            CatWalk.GetComponent<SpriteRenderer>().sortingOrder = 2;
            FoxWalk.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

}
