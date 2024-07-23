using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateController : MonoBehaviour
{
    [SerializeField] private GameObject UI_FadeInOut;
    private ISceneState m_State;

    private void Awake()
    {
        UI_FadeInOut = GameObject.Find("UI_FadeInOut");
    }

    private void Start()
    {
        if (UI_FadeInOut == null)
        {
            UI_FadeInOut = GameObject.Find("UI_FadeInOut(Clone)");
        }
    }

    public SceneStateController() { }

    public void SetSceneState(ISceneState State, string LoadSceneName)
    {
        
        LoadScene(LoadSceneName);

        m_State = State;
    }

    private void LoadScene(string LoadSceneName)
    {
        if (LoadSceneName.Length <= 0) return;
        UI_FadeInOut.GetComponent<UI_FadeInOutAndSceneManage>().FadeIn(LoadSceneName);
    }

    public void SceneStateUpdate()
    {
        if (m_State != null)
        {
            m_State.SceneStateUpdate();
        }
    }
}

public abstract class ISceneState
{
    private string m_SceneStateName = "ISceneState";
    public string sceneStateName
    {
        get { return m_SceneStateName; }
        set { m_SceneStateName = value; }
    }
    protected SceneStateController m_Controller = null;
    public ISceneState(SceneStateController controller)
    {
        m_Controller = controller;
    }

    public virtual void SceneStateUpdate() { }
}

public class InitSceneState : ISceneState 
{ 
    public InitSceneState(SceneStateController controller) : base(controller)
    {
        this.sceneStateName = "InitScene";
    }

    public override void SceneStateUpdate()
    {
        m_Controller.SetSceneState(new TitleSceneState(m_Controller), "TitleScene");
    }
}

public class TitleSceneState : ISceneState
{
    public TitleSceneState(SceneStateController controller) : base(controller)
    {
        this.sceneStateName = "TitleScene";
    }

    public override void SceneStateUpdate()
    {
        m_Controller.SetSceneState(new StorySceneState(m_Controller), "StoryScene");
    }
}

public class StorySceneState : ISceneState
{
    public StorySceneState(SceneStateController controller) : base(controller)
    {
        this.sceneStateName = "StoryScene";
    }

    public override void SceneStateUpdate()
    {
        m_Controller.SetSceneState(new HomeSceneState(m_Controller), "HomeScene");
    }
}

public class HomeSceneState : InitSceneState
{
    public HomeSceneState(SceneStateController controller) : base(controller)
    {
        this.sceneStateName = "HomeScene";
    }

    public override void SceneStateUpdate()
    {
        m_Controller.SetSceneState(new StorySceneState(m_Controller), "StoryScene");
    }
}