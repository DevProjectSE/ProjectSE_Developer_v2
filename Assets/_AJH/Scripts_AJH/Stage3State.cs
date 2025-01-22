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
        Debug.Log("교실에 들어옴 , 0");
    }

    public void Execute()
    {
        //손전등 획득시

        Debug.Log("교탁 자물쇠 해체");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 1)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate1());
        }
    }

    public void Exit()
    {
        Debug.Log("손전등 획득 게임 진행");
    }
}

public class PlayingSate1 : IState
{
    public void Enter()
    {
        Debug.Log("손전등 획득하시오 1");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("손전등 획득");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 2)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate2());
        }
    }

    public void Exit()
    {
        Debug.Log("손전등 획득 종료 2 ");
    }
}

public class PlayingSate2 : IState
{
    public void Enter()
    {
        Debug.Log("일기장을 획득하시오 2");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("일기장 획득 ");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 3)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate3());
        }
    }

    public void Exit()
    {
        Debug.Log("일기장 획득 종료 3");
    }
}

public class PlayingSate3 : IState
{
    public void Enter()
    {
        Debug.Log("버튼 암호 풀기 3");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("버튼 암호 완료");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 4)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate4());
        }
    }

    public void Exit()
    {
        Debug.Log("버튼 암호 풀기 완료 4");
    }
}

public class PlayingSate4 : IState
{
    public void Enter()
    {
        Debug.Log("사물함 암호 4");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("사물함 암호 풀기 완료 4");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 5)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate5());
        }
    } 


    public void Exit()
    {
        Debug.Log("사물함 암호풀기 완료 5  ");
    }
}

public class PlayingSate5 : IState
{
    public void Enter()
    {
        Debug.Log("책가방 조합 5");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("책가방 조합 완료 5");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 6)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate6());
        }
    }

    public void Exit()
    {
        Debug.Log("책가방 조합 종료6");
    }
}

public class PlayingSate6 : IState
{
    public void Enter()
    {
        Debug.Log("칼로 로프자르기 6");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("칼로 로프자리기 완료7");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 7)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSate7());
        }
    }

    public void Exit()
    {
        Debug.Log("칼로 로프자르기 종료 7 ");
    }
}
public class PlayingSate7 : IState
{
    public void Enter()
    {
        Debug.Log("로봇획득 7 ");
    }

    public void Execute()
    {
        //손전등 획득시
        Debug.Log("로봇 획득 완료");
        //Stage3StateManager.Instance.stage3Step++;
        if (Stage3StateManager.Instance.stage3Step == 8)
        {
            Stage3StateManager.Instance.TransitionToState(new PlayingSateEnd());
        }
    }

    public void Exit()
    {
        Debug.Log("로봇 획득 종료");
    }
}
public class PlayingSateEnd : IState
{
    public void Enter()
    {
        Debug.Log("스테이지3 클리어 다음 스테이지로 진입하시오");
    }

    public void Execute()
    {
        //손전등 획득시
        //Debug.Log("첫번째 상태 클리어");
    }

    public void Exit()
    {
        Debug.Log("종료");
    }
}




