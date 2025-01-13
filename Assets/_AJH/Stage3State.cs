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
        Debug.Log("교실에 들어옴");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("손전등 획득");
        Stage3GameManager.Instance.TransitionToState(new PlayingSate1());
    }

    public void Exit()
    {
        Debug.Log("손전등 획등 게임 진행");
    }
}

public class PlayingSate1 : IState
{
    public void Enter()
    {
        Debug.Log("손전등 획득 첫번째 상태");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("첫번째 상태 클리어");
        Stage3GameManager.Instance.TransitionToState(new PlayingSate2());
    }

    public void Exit()
    {
        Debug.Log("첫번째 상태 종료");
    }
}

public class PlayingSate2 : IState
{
    public void Enter()
    {
        Debug.Log("두번째 상태");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("첫번째 상태 클리어");
        Stage3GameManager.Instance.TransitionToState(new Stage3End());
    }

    public void Exit()
    {
        Debug.Log("두번째 상태 종료");
    }
}

public class Stage3End : IState
{
    public void Enter()
    {
        Debug.Log("스테이지3 클리어 다음 스테이지로 진입하시오");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("첫번째 상태 클리어");
    }

    public void Exit()
    {
        Debug.Log("두번째 상태 종료");
    }
}


