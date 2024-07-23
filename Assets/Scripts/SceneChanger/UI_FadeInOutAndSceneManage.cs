using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_FadeInOutAndSceneManage : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool locked = false;
    private string _LoadSceneName;

    public void FadeIn(string LoadSceneName)
    {
        _LoadSceneName = LoadSceneName;
        if (_LoadSceneName == null || _LoadSceneName.Length <= 0 || locked == true) return;
        animator.SetTrigger("FadeOut");
    }

    public void AnimationStart()
    {
        locked = true;
    }

    public void AnimationFinishChangeScene()
    {
        locked = false;
        if (_LoadSceneName == null || _LoadSceneName.Length <= 0 || locked == true) return;
        SceneManager.LoadScene(_LoadSceneName);
        _LoadSceneName = null;
    }

    public void ForceTrigger()
    {
        animator.SetTrigger("FadeOut");
    }

    public bool LockedCheck()
    {
        return locked;
    }
}
