using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAssetFactory
{
    public abstract AudioClip LoadMusicAudio(string ClipName);
    public abstract AudioClip LoadSoundAudio(string ClipName);
    public abstract Sprite LoadFaceSprite(string SpriteName);

    public abstract GameObject LoadFoodObj(string FoodObjName);
}

public class ResourceAssetFactory : IAssetFactory
{
    public const string MusicPath = "Music/";
    public const string SoundPath = "Sound/";
    public const string FaceSpritePath = "CharacterFace/";
    public const string FoodObjPath = "Prefab/CreateObj/";

    public override AudioClip LoadMusicAudio(string ClipName)
    {
        throw new System.NotImplementedException();
    }

    public override AudioClip LoadSoundAudio(string ClipName)
    {
        throw new System.NotImplementedException();
    }

    public override Sprite LoadFaceSprite(string SpriteName)
    {
        throw new System.NotImplementedException();
    }

    public Object LoadGameObjectFromResourcePath(string AssetPath)
    {
        Object res = Resources.Load(AssetPath);
        if (res == null)
        {
            Debug.LogWarning("無法載入路徑[" + AssetPath + "]上的Asset");
            return null;
        }
        return res;
    }

    public override GameObject LoadFoodObj(string FoodObjName)
    {
        throw new System.NotImplementedException();
    }
}

public class ResourceAssetProxyFactory : IAssetFactory
{
    private ResourceAssetFactory m_RealFactory = null;
    private Dictionary<string, AudioClip> m_MusicAudio = null;
    private Dictionary<string, AudioClip> m_SoundAudio = null;
    private Dictionary<string, Sprite> m_LoadFaceSprite = null;
    private Dictionary<string, Object> m_LoadFoodObj = null;

    public ResourceAssetProxyFactory()
    {
        m_RealFactory = new ResourceAssetFactory();
        m_MusicAudio = new Dictionary<string, AudioClip>();
        m_SoundAudio = new Dictionary<string, AudioClip>();
        m_LoadFaceSprite = new Dictionary<string, Sprite>();
        m_LoadFoodObj = new Dictionary<string, Object>();
    }

    public override AudioClip LoadMusicAudio(string ClipName)
    {
        if (m_MusicAudio.ContainsKey(ClipName) == false)
        {
            Object res = m_RealFactory.LoadGameObjectFromResourcePath(ResourceAssetFactory.MusicPath + ClipName);
            m_MusicAudio.Add(ClipName, res as AudioClip);
        }
        return m_MusicAudio[ClipName];
    }

    public override AudioClip LoadSoundAudio(string ClipName)
    {
        if (m_SoundAudio.ContainsKey(ClipName) == false)
        {
            Object res = m_RealFactory.LoadGameObjectFromResourcePath(ResourceAssetFactory.SoundPath  + ClipName);
            m_SoundAudio.Add(ClipName, res as AudioClip);
        }
        return m_SoundAudio[ClipName];
    }

    public override Sprite LoadFaceSprite(string SpriteName)
    {
        if(m_LoadFaceSprite.ContainsKey(SpriteName) == false)
        {
            Sprite res = m_RealFactory.LoadGameObjectFromResourcePath(ResourceAssetFactory.FaceSpritePath + SpriteName) as Sprite;
            m_LoadFaceSprite.Add(SpriteName, res);
        }
        return m_LoadFaceSprite[SpriteName];
    }

    public override GameObject LoadFoodObj(string FoodObjName)
    {
        if (m_LoadFoodObj.ContainsKey(FoodObjName) == false)
        {
            Object res = m_RealFactory.LoadGameObjectFromResourcePath(ResourceAssetFactory.FoodObjPath + FoodObjName);
            m_LoadFoodObj.Add(FoodObjName, res);
        }
        return m_LoadFoodObj[FoodObjName] as GameObject;
    }
}