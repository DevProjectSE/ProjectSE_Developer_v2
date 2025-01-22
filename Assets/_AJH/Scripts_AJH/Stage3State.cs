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
        Debug.Log("���ǿ� ���� , 0");
    }

    public void Execute()
    {
        //������ ȹ���

        Debug.Log("��Ź �ڹ��� ��ü");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 1)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate1());
        }
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
        Debug.Log("������ ȹ���Ͻÿ� 1");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("������ ȹ��");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 2)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate2());
        }
    }

    public void Exit()
    {
        Debug.Log("������ ȹ�� ���� 2 ");
    }
}

public class PlayingSate2 : IState
{
    public void Enter()
    {
        Debug.Log("�ϱ����� ȹ���Ͻÿ� 2");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("�ϱ��� ȹ�� ");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 3)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate3());
        }
    }

    public void Exit()
    {
        Debug.Log("�ϱ��� ȹ�� ���� 3");
    }
}

public class PlayingSate3 : IState
{
    public void Enter()
    {
        Debug.Log("��ư ��ȣ Ǯ�� 3");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("��ư ��ȣ �Ϸ�");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 4)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate4());
        }
    }

    public void Exit()
    {
        Debug.Log("��ư ��ȣ Ǯ�� �Ϸ� 4");
    }
}

public class PlayingSate4 : IState
{
    public void Enter()
    {
        Debug.Log("�繰�� ��ȣ 4");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("�繰�� ��ȣ Ǯ�� �Ϸ� 4");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 5)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate5());
        }
    } 


    public void Exit()
    {
        Debug.Log("�繰�� ��ȣǮ�� �Ϸ� 5  ");
    }
}

public class PlayingSate5 : IState
{
    public void Enter()
    {
        Debug.Log("å���� ���� 5");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("å���� ���� �Ϸ� 5");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 6)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate6());
        }
    }

    public void Exit()
    {
        Debug.Log("å���� ���� ����6");
    }
}

public class PlayingSate6 : IState
{
    public void Enter()
    {
        Debug.Log("Į�� �����ڸ��� 6");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("Į�� �����ڸ��� �Ϸ�7");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 7)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate7());
        }
    }

    public void Exit()
    {
        Debug.Log("Į�� �����ڸ��� ���� 7 ");
    }
}
public class PlayingSate7 : IState
{
    public void Enter()
    {
        Debug.Log("�κ�ȹ�� 7 ");
    }

    public void Execute()
    {
        //������ ȹ���
        Debug.Log("�κ� ȹ�� �Ϸ�");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 8)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSateEnd());
        }
    }

    public void Exit()
    {
        Debug.Log("�κ� ȹ�� ����");
    }
}
public class PlayingSateEnd : IState
{
    public void Enter()
    {
        Debug.Log("��������3 Ŭ���� ���� ���������� �����Ͻÿ�");
    }

    public void Execute()
    {
        //������ ȹ���
        //Debug.Log("ù��° ���� Ŭ����");
    }

    public void Exit()
    {
        Debug.Log("����");
    }
}




