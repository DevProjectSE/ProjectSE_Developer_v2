using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3GameManager : MonoBehaviour
{
    public static Stage3GameManager Instance { get; private set; }

    public IState currentState;

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