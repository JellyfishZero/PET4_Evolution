using UnityEngine;

public abstract class IButtonFunction
{
    protected AudioClip soundEffect;


    public void DoEffect(AudioClip theSound)
    {
        SetSoundRoute(theSound);
        PlaySound();
        ButtonMainFunction();
    }

    public void DoEffect(AudioClip theSound, StoryContentManager storyContentManager)
    {
        SetSoundRoute(theSound);
        PlaySound();
        ButtonMainFunction(storyContentManager);
    }

    protected void SetSoundRoute(AudioClip theSound)
    {
        soundEffect = theSound;
    }

    protected void PlaySound()
    {
        if (soundEffect == null) return;
        AudioSource audioSource = SoundEffectSingleton.Instance.Sound_Manager;
        audioSource.PlayOneShot(soundEffect);
    }

    protected abstract void ButtonMainFunction();
    protected abstract void ButtonMainFunction(StoryContentManager storyContentManager);
}


//TitleScene
public class BtnRestartFunction : IButtonFunction
{
    SceneStateController sceneStateController = GameManagerSingleton.Instance.SceneStateController;
    AudioSource bgmController = GameManagerSingleton.Instance.BGM_Manager;
    protected override void ButtonMainFunction()
    {
        bgmController.Stop();
        sceneStateController.SceneStateUpdate();
        GameManagerSingleton.Instance.GameProcess.resetAll();
    }

    protected override void ButtonMainFunction(StoryContentManager storyContentManager)
    {
        throw new System.NotImplementedException();
    }
}