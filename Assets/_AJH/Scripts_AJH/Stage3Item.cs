using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3Item : MonoBehaviour
{

    public static Stage3Item Instance { get; private set; }

    public int ropeNum = 4;
    public int stageState = 0;

    public GameObject[] lockPad1;

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

}
