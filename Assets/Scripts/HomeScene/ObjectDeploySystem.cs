using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeploySystem : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    bool bDecision;

    IAssetFactory assetFactory = new ResourceAssetProxyFactory();

    GameObject LimitT;
    GameObject LimitD;
    GameObject LimitL;
    GameObject LimitR;

    private void Start()
    {
        LimitT = GameObject.Find("LimitT");
        LimitD = GameObject.Find("LimitD");
        LimitL = GameObject.Find("LimitL");
        LimitR = GameObject.Find("LimitR");
    }

    private void Update()
    {
        if (!bDecision)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
                HomeSceneButton.HomeUIUse(true);
            }
        }
        if (transform.position.x < LimitL.transform.position.x || transform.position.x > LimitR.transform.position.x || transform.position.y < LimitD.transform.position.y || transform.position.y > LimitT.transform.position.y)
        {
            //Debug.LogError("out");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.4f, 0.3f, 1);
            if (Input.GetMouseButtonDown(0))
            {
                SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(assetFactory.LoadSoundAudio("Buzzer1"));
            }
        }
        else
        {
            //Debug.Log("in");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            if (Input.GetMouseButtonDown(0) && !bDecision)
            {
                SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(assetFactory.LoadSoundAudio("Decision2"));
                transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
                bDecision = true;
                HomeManager.AIControllerNotifyFoodIsCome(gameObject);
            }
        }
    }
}
