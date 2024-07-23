using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveAndLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int StoryChapter = 0;
        public int StoryProcess = 0;
        public int CatLove = 0;
        public int FoxLove = 0;
    }

    public bool Save()
    {
        PlayerData playerData = new PlayerData();
        playerData.StoryChapter = GameManagerSingleton.Instance.GameProcess.StoryChapter;
        playerData.StoryProcess = GameManagerSingleton.Instance.GameProcess.StoryProcess;
        playerData.CatLove = GameManagerSingleton.Instance.GameProcess.CatLove;
        playerData.FoxLove = GameManagerSingleton.Instance.GameProcess.FoxLove;

        var serizliedData = JsonUtility.ToJson(playerData);
        var raws = Encoding.UTF8.GetBytes(serizliedData);
        return Save(raws);
    }

    bool Save(byte[] serizliedData)
    {
        const string fileName = "gamesave.dat";
        var filePath = Application.persistentDataPath + "/" + fileName;

        File.WriteAllBytes(filePath, serizliedData);
        Debug.Log("write file success");
        return true;
    }


    public bool Load()
    {
        const string fileName = "gamesave.dat";
        var filePath = Application.persistentDataPath + "/" + fileName;
        //byte[] serizliedData = (byte[])(null);
        string serizliedData;

        // Load
        try
        {
            serizliedData = File.ReadAllText(filePath);
            Debug.Log(serizliedData);
        }
        catch (System.IO.FileNotFoundException)
        {
            Debug.Log("No SaveData");
            return false;
        }
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(serizliedData);

        GameManagerSingleton.Instance.GameProcess.StoryChapter = playerData.StoryChapter;
        GameManagerSingleton.Instance.GameProcess.StoryProcess = playerData.StoryProcess;
        GameManagerSingleton.Instance.GameProcess.CatLove = playerData.CatLove;
        GameManagerSingleton.Instance.GameProcess.FoxLove = playerData.FoxLove;
        return true;
    }

    public bool IsSaveDataExist()
    {
        const string fileName = "gamesave.dat";
        string filePath = Application.persistentDataPath + "/" + fileName;

        return System.IO.File.Exists(filePath);
    }
}
