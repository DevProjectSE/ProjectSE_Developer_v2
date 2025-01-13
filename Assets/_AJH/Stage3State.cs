using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();

}

public class EnterState : IState
{
    public void Enter()
    {
        Debug.Log("���ǿ� ����");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("������ ȹ��");
        Stage3GameManager.Instance.TransitionToState(new PlayingSate1());
    }

    public void Exit()
    {
        Debug.Log("������ ȹ�� ���� ����");
    }
}

public class PlayingSate1 : IState
{
    public void Enter()
    {
        Debug.Log("������ ȹ�� ù��° ����");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("ù��° ���� Ŭ����");
        Stage3GameManager.Instance.TransitionToState(new PlayingSate2());
    }

    public void Exit()
    {
        Debug.Log("ù��° ���� ����");
    }
}

public class PlayingSate2 : IState
{
    public void Enter()
    {
        Debug.Log("�ι�° ����");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("ù��° ���� Ŭ����");
        Stage3GameManager.Instance.TransitionToState(new Stage3End());
    }

    public void Exit()
    {
        Debug.Log("�ι�° ���� ����");
    }
}

public class Stage3End : IState
{
    public void Enter()
    {
        Debug.Log("��������3 Ŭ���� ���� ���������� �����Ͻÿ�");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("ù��° ���� Ŭ����");
    }

    public void Exit()
    {
        Debug.Log("�ι�° ���� ����");
    }
}


