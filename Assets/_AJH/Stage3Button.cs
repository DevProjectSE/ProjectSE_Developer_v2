using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Button : MonoBehaviour
{
    public GameObject buttonUI;


    private void Start()
    {
        buttonUI.SetActive(false);
    }
    public void ShowUI()
    {
        buttonUI.SetActive(true);
    }
}
