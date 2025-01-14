using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    public Rigidbody m_Rb { get; private set; }
    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }
}
