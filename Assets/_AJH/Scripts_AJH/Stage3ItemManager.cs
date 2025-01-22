using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3ItemManager : MonoBehaviour
{

    public GameObject knife;
    public GameObject diary;
    public GameObject flash;
    public GameObject book;
    public GameObject backpack;
    public GameObject homework;

    public static Stage3ItemManager Instance { get; private set; }

    public int ropeNum = 4;
    public int stageState = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }

    

}
