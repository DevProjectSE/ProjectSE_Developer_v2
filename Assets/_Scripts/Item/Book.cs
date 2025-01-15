using System.Collections;
using System.Collections.Generic;
using echo17.EndlessBook;
using UnityEngine;

public class Book : MonoBehaviour
{
    private EndlessBook endlessBook;

    private void Awake()
    {
        endlessBook = GetComponent<EndlessBook>();
    }

    public void OnSelectEnter()
    {
        endlessBook.SetState(EndlessBook.StateEnum.OpenMiddle);
    }
    public void OnSelectExit()
    {
        endlessBook.SetState(EndlessBook.StateEnum.ClosedFront);
    }
}
