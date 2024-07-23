using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryButton : MonoBehaviour
{
    BtnStoryNextFunction btnStoryNextFunction;
    BtnStoryLastFunction btnStoryLastFunction;
    [SerializeField] StoryResourceLoad storyResourceLoad;
    [SerializeField] StoryContentManager storyContentManager;

    private void Awake()
    {
        btnStoryNextFunction = new BtnStoryNextFunction();
        btnStoryLastFunction = new BtnStoryLastFunction();
        storyContentManager = GameObject.Find("StoryManagerObj").GetComponent<StoryContentManager>();
    }

    public void BtnStoryNext()
    {
        btnStoryNextFunction.DoEffect(storyResourceLoad.SE[0], storyContentManager);
    }

    public void BtnStoryLast()
    {
        btnStoryLastFunction.DoEffect(storyResourceLoad.SE[0], storyContentManager);
    }
}

//StoryScene
public class BtnStoryNextFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {
        
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        storyContentManager.nextProcess();
    }
}

public class BtnStoryLastFunction : IButtonFunction
{
    protected override void ButtonMainFunction()
    {

    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        storyContentManager.lastProcess();
    }
}