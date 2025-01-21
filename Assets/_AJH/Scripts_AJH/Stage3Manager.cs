using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public static Stage3Manager Instance { get; private set; }

    public IState currentState;

    public int stage3Step = 0; 
    //��ȣ�� �Ϸ��� ��ȣ ����=0, ��Ź �ڹ���=1, ������ ȹ��=2, �ϱ��� ȹ�� =3, ��ư(��ȣ Ǯ��) =4,
    //�繰�� ��ȣ=5, ������ å����= 6, Į�� �����ڸ��� =7, �κ�ȹ�� = 8

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
