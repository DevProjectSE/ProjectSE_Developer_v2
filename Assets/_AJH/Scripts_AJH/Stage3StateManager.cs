using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3StateManager : MonoBehaviour
{
    public static Stage3StateManager Instance { get; private set; }

    public DialogSystem[] dialogSystems;

    // public IState currentState;

    public int stage3Step = 0;
    public int setNum = 0;

    public bool isTouchDoor = false; //0                완료
    public bool isCheckTeachersdesk = false; //1        완료
    public bool isGetLight = false; //2                 완료
    public bool isCheckHaYunsDesk = false; //3          완료
    public bool isGetDiary = false; //4                   완료
    public bool isDiaryInInventory1 = false; //5        완료
    public bool isGetBook = false; //6                    완료
    public bool isDiaryInInventory2 = false; //7            
    public bool isGetHomework = false; //8              완료
    public bool isDiaryInInventory3 = false; //9        
    public bool isGetBagpack = false; //10              완료
    public bool isSettingHaYunsObject = false; //11     완료   
    public bool isCheckRope = false; //12               완료
    public bool isGetRobot = false; //13                  완료
    public bool isDariyInInventory4 = false; //14 
    public NumberKeyPad backpackLoker;
    public XRKnob backpackLokerKnob;
    public XRGrabInteractable backPack;
    public GameObject robot;
    public XRKnob cleanLockDoor;

    public Stage3ClassDoor stage3ClassDoor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // TransitionToState(new EnterState());s
    }

    private void Update()
    {
        // currentState?.Execute();

        dialogSystems[stage3Step].gameObject.SetActive(true);
        if (dialogSystems[15].isDialogsEnd)
        {
            dialogSystems[16].gameObject.SetActive(true);
            stage3ClassDoor.enabled = true;
        }
    }

    // public void TransitionToState(IState newState)
    // {
    //     currentState?.Exit();

    //     currentState = newState;

    //     currentState.Enter();
    // }

}
