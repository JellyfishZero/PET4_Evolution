using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectSingleton
{
    private GameObject gameObject;

    //單例
    private static SoundEffectSingleton m_Instance;
    //接口
    public static SoundEffectSingleton Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new SoundEffectSingleton();
                if (GameObject.Find("SoundEffectManager") != null)
                {
                    m_Instance.gameObject = GameObject.Find("SoundEffectManager");
                }
                else
                {
                    m_Instance.gameObject = new GameObject("SoundEffectManager");
                }
                m_Instance.gameObject.AddComponent<AudioSource>();
            }
            return m_Instance;
        }
    }

    private AudioSource m_Sound_Manager;

    public AudioSource Sound_Manager
    {
        get
        {
            if (m_Sound_Manager == null)
            {
                m_Sound_Manager = gameObject.GetComponent<AudioSource>();
            }
            return m_Sound_Manager;
        }
    }
}
