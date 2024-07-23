using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    private int _StoryChapter = 0;
    public int StoryChapter
    {
        set
        {
            if (value > 0)
            {
                _StoryChapter = value;
            }
        }
        get
        {
            return _StoryChapter;
        }
    }

    int _StoryProcess = 0;
    public int StoryProcess
    {
        set
        {
            if (value >= 0)
            {
                _StoryProcess = value;
            }
        }
        get
        {
            return _StoryProcess;
        }
    }

    int _CatLove = 0;

    public int CatLove
    {
        set
        {
           _CatLove = value;
        }
        get
        {
            return _CatLove;
        }
    }

    int _FoxLove = 0;

    public int FoxLove
    {
        set
        {
            _FoxLove = value;
        }
        get
        {
            return _FoxLove;
        }
    }

    public void resetAll()
    {
        GameManagerSingleton.Instance.GameProcess.StoryChapter = 0;
        GameManagerSingleton.Instance.GameProcess.StoryProcess = 0;
        GameManagerSingleton.Instance.GameProcess.CatLove = 0;
        GameManagerSingleton.Instance.GameProcess.FoxLove = 0;
    }
}
