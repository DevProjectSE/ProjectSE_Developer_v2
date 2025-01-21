using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public static Stage3Manager Instance { get; private set; }

    public IState currentState;

    public int stage3Step = 0; 
    //번호는 완료후 번호 입장=0, 교탁 자물쇠=1, 손전등 획득=2, 일기장 획득 =3, 버튼(암호 풀기) =4,
    //사물함 암호=5, 조합한 책가방= 6, 칼로 로프자르기 =7, 로봇획득 = 8

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
    }

    public void TransitionToState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }


}
