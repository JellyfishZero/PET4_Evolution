using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    BtnRestartFunction btnRestartFunction;
    IButtonFunction btnCloseFunction;
    [SerializeField] TitlePreLoadMESE ME_SE_Load;
    [SerializeField] Animator TeamUI_Animator;
    [SerializeField] Button LoadBtn;

    private void Awake()
    {
        btnRestartFunction = new BtnRestartFunction();
        btnCloseFunction = new CloseBtnFunction();
    }

    private void Start()
    {
        SaveAndLoadSystem saveAndLoadSystem = new SaveAndLoadSystem();
        LoadBtn.interactable = saveAndLoadSystem.IsSaveDataExist();
    }

    public void BtnGameStart()
    {
        btnRestartFunction.DoEffect(ME_SE_Load.ButtonClick);
    }

    public void BtnGameLoad()
    {
        SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(ME_SE_Load.ButtonClick);
        SaveAndLoadSystem saveAndLoadSystem = new SaveAndLoadSystem();
        saveAndLoadSystem.Load();

        GameManagerSingleton.Instance.SceneStateController.SetSceneState(new HomeSceneState(GameManagerSingleton.Instance.SceneStateController), "HomeScene");
    }

    public void BtnGameExit()
    {
        btnCloseFunction.DoEffect(ME_SE_Load.ButtonClick);
    }

    public void BtnGameTeam()
    {
        SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(ME_SE_Load.ButtonClick);
        TeamUI_Animator.Play("RunTeamName");
    }
}
