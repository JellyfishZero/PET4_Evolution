using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton
{
    private GameObject gameObject;

    //單例
    private static GameManagerSingleton m_Instance;
    //接口
    public static GameManagerSingleton Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManagerSingleton();
                m_Instance.gameObject = new GameObject("GameManager");
                m_Instance.gameObject.AddComponent<SceneStateController>();
                m_Instance.gameObject.AddComponent<AudioSource>();
                m_Instance.gameObject.AddComponent<GameProcess>();
            }
            return m_Instance;  
        }
    }

    private SceneStateController m_SceneStateController;
    public SceneStateController SceneStateController
    {
        get
        {
            if (m_SceneStateController == null)
            {
                m_SceneStateController = gameObject.GetComponent<SceneStateController>();
            }
            return m_SceneStateController;
        }
    }

    private AudioSource m_BGM_Manager;

    public AudioSource BGM_Manager
    {
        get
        {
            if(m_BGM_Manager == null)
            {
                m_BGM_Manager = gameObject.GetComponent<AudioSource>();
            }
            return m_BGM_Manager;
        }
    }

    private GameProcess m_GameProcess;

    public GameProcess GameProcess
    {
        get
        {
            if (m_GameProcess == null)
            {
                m_GameProcess = gameObject.GetComponent<GameProcess>();
            }
            return m_GameProcess;
        }
    }

}
