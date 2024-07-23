using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

enum moveState
{
    WalkRandom,
    Idle,
    LookAtScreen,
    Jump,
    Sleep,
    LookForFood,
}


public class AIController : MonoBehaviour
{
    [SerializeField] HomeHitorTouchUIController homeHitorTouchUIController;
    IAssetFactory assetFactory = new ResourceAssetProxyFactory();

    [SerializeField] GameObject LimitT;
    [SerializeField] GameObject LimitD;
    [SerializeField] GameObject LimitL;
    [SerializeField] GameObject LimitR;

    [SerializeField] float speed = 0.5f;
    [SerializeField] float sleepTimeLimit = 15f;

    float[] Limit_X = new float[2];
    float[] Limit_Y = new float[2];
    float noInteractTimeCount = 0;

    bool DirectionDicisionDone = false;

    moveState _state;

    Animator animator;

    Vector2 TargetLocation;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Limit_X[0] = LimitL.transform.position.x; //左
        Limit_X[1] = LimitR.transform.position.x; //右
        Limit_Y[0] = LimitT.transform.position.y; //後
        Limit_Y[1] = LimitD.transform.position.y; //前
        HomeManager.AIControllerAttach(this);
        //Debug.Log(Limit_X[0] + " " + Limit_X[1] + " " + Limit_Y[0] + " " + Limit_Y[1]);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_state);
        switch (_state)
        {
            case moveState.WalkRandom:
                WalkRandom();
                break;
            case moveState.Idle:
                Idle();
                break;
            case moveState.Sleep:
                FallInSleep();
                break;
            case moveState.LookAtScreen:
                LookAtScreen();
                break;
            case moveState.LookForFood:
                LookForFood();
                break;
        }
        if (HomeManager.foodSearching)
        {
            _state = moveState.LookForFood;
            noInteractTimeCount = 0;
            if (animator.GetBool("Sleep"))
            {
                animator.SetBool("Sleep", false);
            }
        }
        else
        {
            noInteractTimeCount += Time.deltaTime;
        }

    }

    bool isSleep = false;
    //LookAtScreen
    private void LookAtScreen()
    {
        if (DirectionDicisionDone == false)
        {
            DirectionDicisionDone = true;
            if (animator.GetBool("Sleep") == true)
            {
                isSleep = true;
                //Debug.Log("?");
                StartCoroutine("WakeUpAnimation");
            }
            else
            {
                if (isSleep == false)
                {
                    animator.Play("Idle_Front");
                    animator.SetInteger("Direction", 0);
                }
            }
        }
    }

    IEnumerator WakeUpAnimation()
    {
        animator.SetBool("Sleep", false);
        yield return new WaitForSeconds(4f);
        isSleep = true;
    }

    private void FallInSleep()
    {
        if (animator.GetBool("Sleep")==false)
        {
            //Debug.Log(gameObject.name + ": Sleep");
            animator.Play("Idle_Front");
            animator.SetInteger("Direction", 0);
            animator.SetBool("Sleep", true);
        }
        else
        {
            if (DirectionDicisionDone == false)
            {
                DirectionDicisionDone = true;
                StartCoroutine(BeforeSleepSwitch());
            }
        }
    }


    //FallInSleep

    int FoxRealSleep;

    private IEnumerator BeforeSleepSwitch()
    {
        yield return new WaitForSeconds(6f);
        if (gameObject.name == "FoxWalk")
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fox_Special_2"))
            {
                FoxRealSleep += 1;
            }
            if (FoxRealSleep > 3)
            {
                FoxRealSleep = 0;
                animator.SetTrigger("SleepFallToSleep");
            }
        }
        animator.SetTrigger("SleepEffectSwitch");
        DirectionDicisionDone = false;
    }

    //Idle

    void Idle()
    {
        if (DirectionDicisionDone == false)
        {
            //Debug.Log(gameObject.name + ": Idle");
            DirectionDicisionDone = true;
            StartCoroutine("Idle_ChangeStateToWalkRandom");
        }
    }

    IEnumerator Idle_ChangeStateToWalkRandom()
    {
        switch (animator.GetInteger("Direction"))
        {
            case 0:
                animator.Play("Idle_Front");
                break;
            case 1:
                animator.Play("Idle_Left");
                break;
            case 2:
                animator.Play("Idle_Right");
                break;
            case 3:
                animator.Play("Idle_Back");
                break;
        }
        yield return new WaitForSeconds(1.3f);
        DirectionDicisionDone = false;
        if (noInteractTimeCount > sleepTimeLimit)
        {
            _state = moveState.Sleep;
        }
        else
        {
            SetTarget();
            _state = moveState.WalkRandom;
        }
    }


    void SetTarget() //不給參數就隨機
    {
        float x = Random.Range(Limit_X[0], Limit_X[1]);
        float y = Random.Range(Limit_Y[1], Limit_Y[0]);

        TargetLocation = new Vector2(x, y);
    }

    //WalkRandom

    void WalkRandom()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetLocation, Time.deltaTime * speed);

        if (DirectionDicisionDone == false)
        {
            //Debug.Log(gameObject.name + ": WalkRandom");
            float direction_X, direction_Y;
            direction_X = TargetLocation.x - transform.position.x;
            direction_Y = TargetLocation.y - transform.position.y;

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Front") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Back") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Left") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Right"))
            //{
            //    animator.SetTrigger("Walk");
            //}
            if (Mathf.Abs(direction_Y) >= Mathf.Abs(direction_X))
            {
                if (direction_Y <= 0)
                {
                    animator.Play("Idle_Front");
                    animator.SetInteger("Direction", 0);
                }
                else
                {
                    animator.Play("Idle_Back");
                    animator.SetInteger("Direction", 3);
                }
            }
            else
            {
                if (direction_X <= 0)
                {
                    animator.Play("Idle_Left");
                    animator.SetInteger("Direction", 1);
                }
                else
                {
                    animator.Play("Idle_Right");
                    animator.SetInteger("Direction", 2);
                }
            }
            animator.SetTrigger("Walk");
            DirectionDicisionDone = true;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), TargetLocation) < 0.001f)
        {
            //Debug.Log("抵達");
            _state = moveState.Idle;
            DirectionDicisionDone = false;
        }
    }



    void OnMouseOver()
    {
        if (!HomeManager.foodSearching && HomeSceneButton.bHomeUIUse)
        {
            if (clickCharacter == false)
            {
                _state = moveState.LookAtScreen;

                noInteractTimeCount = 0;
                DirectionDicisionDone = false;
            }
        }
    }

    void OnMouseExit()
    {
        if (!HomeManager.foodSearching && HomeSceneButton.bHomeUIUse)
        {
            if (clickCharacter == false)
            {
                //Debug.Log("exit");
                DirectionDicisionDone = false;
            }
            _state = moveState.Idle;
        }
    }

    //Click的時候角色要跳一下
    bool clickCharacter;
    void OnMouseDown()
    {
        if (!HomeManager.foodSearching && HomeSceneButton.bHomeUIUse)
        {
            if (clickCharacter == false)
            {
                SoundEffectSingleton.Instance.Sound_Manager.PlayOneShot(assetFactory.LoadSoundAudio("Cursor2"));
                clickCharacter = true;
                animator.Play("Idle_Front");
                animator.SetInteger("Direction", 0);
                animator.SetTrigger("Jump");
                StartCoroutine("JumpAnimation");
                //_state = moveState.Jump;
            }
        }
    }

    IEnumerator JumpAnimation()
    {
        homeHitorTouchUIController.SetInteractTarget(gameObject);
        homeHitorTouchUIController.ShowMenu();
        yield return new WaitForSeconds(1.3f);
        animator.Play("Idle_Front");
        clickCharacter = false;
        DirectionDicisionDone = false;
    }


    //private void JumpState()
    //{
    //    StartCoroutine(JumpAnimation());
    //}


    //FoodIsCome
    public void FoodIsCome(GameObject food)
    {
        SetTarget(food);
        DirectionDicisionDone = true;
        if (DirectionDicisionDone)
        {
            _state = moveState.LookForFood;
            DirectionDicisionDone = false;
        }
    }

    private void SetTarget(GameObject food)
    {
        float x = food.transform.position.x;
        float y = food.transform.position.y;

        TargetLocation = new Vector2(x, y);
    }

    private void LookForFood()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetLocation, Time.deltaTime * speed * 2);

        if (DirectionDicisionDone == false)
        {
            float direction_X, direction_Y;
            direction_X = TargetLocation.x - transform.position.x;
            direction_Y = TargetLocation.y - transform.position.y;

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Front") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Back") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Left") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Right"))
            //{
            //    animator.SetTrigger("Walk");
            //}
            if (Mathf.Abs(direction_Y) >= Mathf.Abs(direction_X))
            {
                if (direction_Y <= 0)
                {
                    animator.Play("Idle_Front");
                    animator.SetInteger("Direction", 0);
                }
                else
                {
                    animator.Play("Idle_Back");
                    animator.SetInteger("Direction", 3);
                }
            }
            else
            {
                if (direction_X <= 0)
                {
                    animator.Play("Idle_Left");
                    animator.SetInteger("Direction", 1);
                }
                else
                {
                    animator.Play("Idle_Right");
                    animator.SetInteger("Direction", 2);
                }
            }
            animator.SetTrigger("Walk");
            DirectionDicisionDone = true;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), TargetLocation) < 0.001f)
        {
            //Debug.Log("抵達");
            HomeManager.AIControllerNotifyFoodHasFound(gameObject);
        }
    }

    public void StopSearchingFood()
    {
        DirectionDicisionDone = false;
        if (!DirectionDicisionDone)
        {
            _state = moveState.Idle;
        }
    }
}


