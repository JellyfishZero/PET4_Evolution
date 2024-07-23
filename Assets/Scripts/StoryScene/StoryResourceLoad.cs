using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryResourceLoad : MonoBehaviour
{
    //BackGroundImg
    [SerializeField] public List<Sprite> Img_BackGound;

    //headImg
    [SerializeField] public List<Sprite> Img_Face;

    //Music
    [SerializeField] public List<AudioClip> ME;

    //Sound
    [SerializeField] public List<AudioClip> SE;


    private void Awake()
    {
        //ImgLoad(Img_BackGound, "Background/background");
        //ImgLoad(Img_BackGound, "Background/Title");
        SpriteLoad(Img_Face, "CharacterFace/Cat1");
        SpriteLoad(Img_Face, "CharacterFace/Cat2");
        SpriteLoad(Img_Face, "CharacterFace/Cat3");
        SpriteLoad(Img_Face, "CharacterFace/Fox1");
        SpriteLoad(Img_Face, "CharacterFace/Fox2");
        SpriteLoad(Img_Face, "CharacterFace/Fox3");
        SpriteLoad(Img_Face, "CharacterFace/Fox4");
        audioLoad(SE, "Sound/Decision2");
    }

    private void SpriteLoad(List<Sprite> spriteList, string route)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(route);
        foreach (Sprite picture in sprites)
        {
            spriteList.Add(picture);
        }
    }

    private void ImgLoad(List<Sprite> Img_BackGound, string route)
    {
        Sprite sprite = Resources.Load<Sprite>(route);
        Img_BackGound.Add(sprite);
    }

    private void audioLoad(List<AudioClip> audioClipList, string route)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(route);
        audioClipList.Add(audioClip);
    }
}
