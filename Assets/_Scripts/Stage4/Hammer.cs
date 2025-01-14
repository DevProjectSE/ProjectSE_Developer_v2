using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    public Rigidbody rb;
    public TextMeshProUGUI rb_Velocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
