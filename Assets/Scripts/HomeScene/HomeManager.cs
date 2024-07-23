using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    static List<AIController> aIControllers = new List<AIController>();
    static HomeLoveUIController homeLoveUIController;

    static GameObject currentFoodObj;
    static public bool foodSearching; //是不是在找食物的狀態

    public static void SetHomeLoveUIContorller(HomeLoveUIController theHomeLoveUIController)
    {
        homeLoveUIController = theHomeLoveUIController;
    }

    public static void CreateHomeSceneObject(GameObject createObject)
    {
        IAssetFactory assetFactory = new ResourceAssetProxyFactory();
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Instantiate(createObject, mousePosition, Quaternion.Euler(0f, 0f, 0f));
    }

    public static void AIControllerAttach(AIController theAIController)
    {
        aIControllers.Add(theAIController);
    }

    public static void AIControllerNotifyFoodIsCome(GameObject food)//食物用
    {
        homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.FindFood_Dialog, homeLoveUIController.Fox_Dialog.FindFood_Dialog);
        homeLoveUIController.SetShowDialog(true);
        foodSearching = true;
        currentFoodObj = food;
        foreach (AIController controller in aIControllers)
        {
            //Debug.Log(controller.name);
            controller.FoodIsCome(food);
        }
    }

    public static void AIControllerNotifyFoodHasFound(GameObject whofound)//角色說她找到了
    {
        foodSearching = false;
        HomeSceneButton.HomeUIUse(true);
        foreach (AIController controller in aIControllers)
        {
            controller.StopSearchingFood();
        }

        //加分
        if (currentFoodObj.name.Replace("(Clone)", "").CompareTo("canObj") == 0)
        {
            LoveAdjust(whofound, 10);
        }
        else if (currentFoodObj.name.Replace("(Clone)", "").CompareTo("foodObj") == 0)
        {
            LoveAdjust(whofound, 15);
        }
        //更新UI
        if (whofound.name == "CatWalk")
        {
            homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.GetFood_Dialog, homeLoveUIController.Fox_Dialog.LostFood_Dialog);
            homeLoveUIController.SetShowDialog(true);
        }
        else if (whofound.name == "FoxWalk")
        {
            homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.LostFood_Dialog, homeLoveUIController.Fox_Dialog.GetFood_Dialog);
            homeLoveUIController.SetShowDialog(true);
        }
        LoveValueCheck();

        Destroy(currentFoodObj);
    }

    public enum Interaction
    {
        Touch,
        Hit,
    }

    public static void InteractWith(GameObject gameObject, Interaction interaction)
    {
        if(gameObject.name.CompareTo("CatWalk") == 0)
        {
            CatInteract(gameObject, interaction);
        }
        if(gameObject.name.CompareTo("FoxWalk") == 0)
        {
            FoxInteract(gameObject, interaction);
        }
    }

    private static void CatInteract(GameObject gameObject, Interaction interaction)
    {
        switch (interaction)
        {
            case Interaction.Touch:
                //加分
                LoveAdjust(gameObject, 8);
                //更新UI
                if (GameManagerSingleton.Instance.GameProcess.CatLove <= 30)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.Touch_30_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.CatLove > 30 && GameManagerSingleton.Instance.GameProcess.CatLove <= 70)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.Touch_60_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.CatLove > 70 && GameManagerSingleton.Instance.GameProcess.CatLove < 100)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.Touch_100_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.CatLove >= 100)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.Touch_max, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                LoveValueCheck();
                break;
            case Interaction.Hit:
                //加分
                LoveAdjust(gameObject, -20);
                //更新UI
                homeLoveUIController.UpdateContent(homeLoveUIController.Cat_Dialog.Hit_Dialog, gameObject.name);
                homeLoveUIController.SetShowDialog(true, gameObject.name);
                LoveValueCheck();
                break;
        }
    }

    private static void FoxInteract(GameObject gameObject, Interaction interaction)
    {
        switch (interaction)
        {
            case Interaction.Touch:
                //加分
                LoveAdjust(gameObject, 8);
                //更新UI
                if (GameManagerSingleton.Instance.GameProcess.FoxLove <= 30)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Fox_Dialog.Touch_30_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.FoxLove > 30 && GameManagerSingleton.Instance.GameProcess.FoxLove <= 70)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Fox_Dialog.Touch_60_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.FoxLove > 70 && GameManagerSingleton.Instance.GameProcess.FoxLove < 100)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Fox_Dialog.Touch_100_Dialog, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                if (GameManagerSingleton.Instance.GameProcess.FoxLove >= 100)
                {
                    homeLoveUIController.UpdateContent(homeLoveUIController.Fox_Dialog.Touch_max, gameObject.name);
                    homeLoveUIController.SetShowDialog(true, gameObject.name);
                }
                LoveValueCheck();
                break;
            case Interaction.Hit:
                //加分
                LoveAdjust(gameObject, -20);
                //更新UI
                homeLoveUIController.UpdateContent(homeLoveUIController.Fox_Dialog.Hit_Dialog, gameObject.name);
                homeLoveUIController.SetShowDialog(true, gameObject.name);
                LoveValueCheck();
                break;
        }
    }

    public static void LoveAdjust(GameObject gameObject, int loveScore)
    {
        if(gameObject.name.CompareTo("CatWalk") == 0)
        {
            GameManagerSingleton.Instance.GameProcess.CatLove += loveScore;
            if (GameManagerSingleton.Instance.GameProcess.CatLove > 100)
            {
                GameManagerSingleton.Instance.GameProcess.CatLove = 100;
            }
            if (GameManagerSingleton.Instance.GameProcess.CatLove < 0)
            {
                GameManagerSingleton.Instance.GameProcess.CatLove = 0;
            }
        }
        if(gameObject.name.CompareTo("FoxWalk") == 0)
        {
            GameManagerSingleton.Instance.GameProcess.FoxLove += loveScore;
            if (GameManagerSingleton.Instance.GameProcess.FoxLove > 100)
            {
                GameManagerSingleton.Instance.GameProcess.FoxLove = 100;
            }
            if (GameManagerSingleton.Instance.GameProcess.FoxLove < 0)
            {
                GameManagerSingleton.Instance.GameProcess.FoxLove = 0;
            }
        }
    }

    private static void LoveValueCheck()
    {
        if(GameManagerSingleton.Instance.GameProcess.CatLove >= 100 && GameManagerSingleton.Instance.GameProcess.FoxLove >= 100)
        {
            homeLoveUIController.SetUnlockR18Btn(true);
        }
        else
        {
            homeLoveUIController.SetUnlockR18Btn(false);
        }
    }
}
