using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3StateManager : MonoBehaviour
{
    public static Stage3StateManager Instance { get; private set; }

    public DialogSystem[] dialogSystems;

    public IState currentState;

    public int stage3Step = 0;

    public bool isTouchDoor = false; //1
    public bool isCheckTeachersdesk = false; //2
    public bool isCheckHaYunsDesk = false; //4

    public bool isDariyInInventory1 = false; //6
    public bool isDariyInInventory2 = false; //8
    public bool isDariyInInventory3 = false; //10
    public bool isDariyInInventory4 = false; //15
    public bool isSettingHaYunsObject = false; //12


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
