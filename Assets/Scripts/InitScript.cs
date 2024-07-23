using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
    [SerializeField] GameObject UI_FadeInOut;

    SceneStateController sceneStateController;
    private void Awake()
    {
        GameObject soundEffectManager = new GameObject("SoundEffectManager");
        sceneStateController = GameManagerSingleton.Instance.SceneStateController;
        DontDestroyOnLoad(soundEffectManager);
        DontDestroyOnLoad(sceneStateController);
        DontDestroyOnLoad(UI_FadeInOut);
    }

    void Start()
    {
        sceneStateController.SetSceneState(new InitSceneState(sceneStateController), "");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI_FadeInOut.GetComponent<UI_FadeInOutAndSceneManage>().LockedCheck())
        {
            sceneStateController.SceneStateUpdate();
        }
    }
}
