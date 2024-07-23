using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HomeDialog{
    public string sprite;
    public string dialog;
}

public class HomeSceneDialogReader
{
    IAssetFactory assetFactory = new ResourceAssetProxyFactory();
    public HomeDialog Default_Dialog;
    public List<HomeDialog> FindFood_Dialog = new List<HomeDialog>();
    public List<HomeDialog> GetFood_Dialog = new List<HomeDialog>();
    public List<HomeDialog> LostFood_Dialog = new List<HomeDialog>();
    public List<HomeDialog> Touch_30_Dialog = new List<HomeDialog>();
    public List<HomeDialog> Touch_60_Dialog = new List<HomeDialog>();
    public List<HomeDialog> Touch_100_Dialog = new List<HomeDialog>();
    public List<HomeDialog> Touch_max = new List<HomeDialog>();
    public List<HomeDialog> Hit_Dialog = new List<HomeDialog>();

    public HomeSceneDialogReader(TextAsset textAsset)
    {
        string textContent = textAsset.text;
        string[] lines = textContent.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            string[] lineContent = lines[i].Split(',');
            Classification(lineContent);
        }
    }

    private void Classification(string[] lineContent)
    {
        if(lineContent[0].CompareTo("Default") == 0)
        {
            Default_Dialog.sprite = lineContent[1];
            Default_Dialog.dialog = lineContent[2];
            return;
        }

        HomeDialog temp;
        temp.sprite = lineContent[1];
        temp.dialog = lineContent[2];
        switch (lineContent[0])
        {
            case "FindFood":
                FindFood_Dialog.Add(temp);
                break;
            case "GetFood":
                GetFood_Dialog.Add(temp);
                break;
            case "LostFood":
                LostFood_Dialog.Add(temp);
                break;
            case "Touch_30":
                Touch_30_Dialog.Add(temp);
                break;
            case "Touch_60":
                Touch_60_Dialog.Add(temp);
                break;
            case "Touch_100":
                Touch_100_Dialog.Add(temp);
                break;
            case "Touch_max":
                Touch_max.Add(temp);
                break;
            case "Hit":
                Hit_Dialog.Add(temp);
                break;
        }
    }
}
