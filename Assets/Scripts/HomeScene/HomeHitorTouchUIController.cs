using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeHitorTouchUIController : MonoBehaviour
{
    private ResourceAssetProxyFactory resourceAssetProxyFactory;
    private GameObject interactTarget;

    private BtnTouchMethod btnTouchMethod;
    private BtnHitMethod btnHitMethod;

    private void Awake()
    {
        resourceAssetProxyFactory = new ResourceAssetProxyFactory();
        btnTouchMethod = new BtnTouchMethod();
        btnHitMethod = new BtnHitMethod();
    }

    public void SetInteractTarget(GameObject gameObject)
    {
        interactTarget = gameObject;
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public void BtnTouch()
    {
        btnTouchMethod.SetInteractTarget(interactTarget);
        btnTouchMethod.DoEffect(resourceAssetProxyFactory.LoadSoundAudio("Holy5"));
        gameObject.SetActive(false);
    }

    public void BtnHit()
    {
        btnHitMethod.SetInteractTarget(interactTarget);
        btnHitMethod.DoEffect(resourceAssetProxyFactory.LoadSoundAudio("Blow1"));
        gameObject.SetActive(false);
    }
}

class BtnTouchMethod : IButtonFunction
{
    GameObject interactTarget;
    public void SetInteractTarget(GameObject gameObject)
    {
        interactTarget = gameObject;
    }

    protected override void ButtonMainFunction()
    {
        HomeManager.InteractWith(interactTarget, HomeManager.Interaction.Touch);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}

class BtnHitMethod : IButtonFunction
{
    GameObject interactTarget;
    public void SetInteractTarget(GameObject gameObject)
    {
        interactTarget = gameObject;
    }

    protected override void ButtonMainFunction()
    {
        HomeManager.InteractWith(interactTarget, HomeManager.Interaction.Hit);
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}