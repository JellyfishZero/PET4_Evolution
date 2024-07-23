using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenInit : MonoBehaviour
{
    [SerializeField] GameObject fadeInOutPrebab;
    //SceneStateController sceneStateController;
    GameObject fadeInOutManager;

    [SerializeField]
    TitlePreLoadMESE ME_SE_Load;
    AudioSource BGM_Manager;

    bool testMode;

    private void Awake()
    {
        fadeInOutManager = GameObject.Find("UI_FadeInOut");
        if (fadeInOutManager == null)
        {
            Instantiate(fadeInOutPrebab);
            fadeInOutManager = GameObject.Find("UI_FadeInOut(Clone)");
            testMode = true;
        }
        BGM_Manager = GameManagerSingleton.Instance.BGM_Manager;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!testMode)
        {
            fadeInOutManager.GetComponent<UI_FadeInOutAndSceneManage>().ForceTrigger();
        }

        BGM_Manager.clip = ME_SE_Load.TitleMusic;//TitlePreLoadMESE.TitlePreLoadMESE_instance.MusicClips["TitleMusic"].clip; //ME_SE_Load.MusicClips["TitleMusic"].clip;
        BGM_Manager.loop = true;
        BGM_Manager.volume = .3f;
        BGM_Manager.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
