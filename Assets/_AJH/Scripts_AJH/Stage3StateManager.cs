using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3StateManager : MonoBehaviour
{
    public static Stage3StateManager Instance { get; private set; }

    public DialogSystem[] dialogSystems;

    public IState currentState;

    public int stage3Step = 0;
    /*
    * 
    * // 입장 = 0 //
    * 문잡기 = 1//
    * 교탁에 진입 = 2// 
    * // 손전등획득 = 3 //
    * 하윤이 책상근처 진입 = 4// 
    * // 일기장 획득 =5 //
    * 일기장 인벤토리 수납 = 6//
    * // 교과서 획득 = 7 //
    * 일기장 인벤토리 수납 = 8//
    * // 숙제 유인물 아이템 획득 = 9 //
    * 일기장 인벤토리 수납 = 10 //
    * // 가방 획득 = 11 //
    * 물건 배치 완료 = 12
    * 줄오브젝트 앞에 서면 = 13
    * // 로봇 아이템 획득 = 14 //
    * 일기장 수납 = 15//
    * 마무리 = 16
    */


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
