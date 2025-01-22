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
    * // ���� = 0 //
    * ����� = 1//
    * ��Ź�� ���� = 2// 
    * // ������ȹ�� = 3 //
    * ������ å���ó ���� = 4// 
    * // �ϱ��� ȹ�� =5 //
    * �ϱ��� �κ��丮 ���� = 6//
    * // ������ ȹ�� = 7 //
    * �ϱ��� �κ��丮 ���� = 8//
    * // ���� ���ι� ������ ȹ�� = 9 //
    * �ϱ��� �κ��丮 ���� = 10 //
    * // ���� ȹ�� = 11 //
    * ���� ��ġ �Ϸ� = 12
    * �ٿ�����Ʈ �տ� ���� = 13
    * // �κ� ������ ȹ�� = 14 //
    * �ϱ��� ���� = 15//
    * ������ = 16
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
