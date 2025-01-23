using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3StateManager : MonoBehaviour
{
    public static Stage3StateManager Instance { get; private set; }

    public DialogSystem[] dialogSystems;

    public IState currentState;

    public int stage3Step = 0;
    public int setNum = 0;

    public bool isTouchDoor = false; //0                �Ϸ�
    public bool isCheckTeachersdesk = false; //1        �Ϸ�
    public bool isGetLight = false; //2                 �Ϸ�
    public bool isCheckHaYunsDesk = false; //3          �Ϸ�
    public bool isGetDiary=false; //4                   �Ϸ�
    public bool isDiaryInInventory1 = false; //5        
    public bool isGetBook=false; //6                    �Ϸ�
    public bool isDiaryInInventory2 = false; //7            
    public bool isGetHomework = false; //8              �Ϸ�
    public bool isDiaryInInventory3 = false; //9        
    public bool isGetBagpack = false; //10              �Ϸ�
    public bool isSettingHaYunsObject = false; //11     �Ϸ�   
    public bool isCheckRope = false; //12               �Ϸ�
    public bool isGetRobot=false; //13                  �Ϸ�
    public bool isDariyInInventory4 = false; //14 

    private void Awake()
    {
        if(Instance == null)
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
        TransitionToState(new EnterState());
    }

    private void Update()
    {
        currentState?.Execute();
   

        dialogSystems[stage3Step].gameObject.SetActive(true);
           
    }

    

    public void TransitionToState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }


}
