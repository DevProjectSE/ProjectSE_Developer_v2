using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Lock : MonoBehaviour
{
    public GameObject inputPassword;
    public int Passwoard;
    public bool isOpen=false;


    private void Awake()
    {
        inputPassword.SetActive(false);
    }

    private void Update()
    {
        if ((false))
        {
            OpenUI();
        }
        else
        {
            CloseUI();
        }
    }

    private void OpenUI()
    {
        inputPassword.SetActive(true);
    }

    private void CloseUI()
    {
        inputPassword.SetActive(false);
    }

}
