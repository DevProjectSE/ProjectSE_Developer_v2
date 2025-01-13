using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private Vector3 m_LastPosition;
    public float m_Speed;
    public TextMeshProUGUI m_MeterPerSecond, m_KillometersperHour;
    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        m_Speed = GetSpeed();
        m_MeterPerSecond.text = string.Format("{0:00.00} m/s", m_Speed);
        m_KillometersperHour.text = string.Format("{0:00.00} km/h", m_Speed * 3.6f);
    }

    private float GetSpeed()
    {
        float speed = (transform.position - m_LastPosition).magnitude / Time.deltaTime;
        m_LastPosition = transform.position;

        return speed;
    }

    private void OnCollisionEnter(Collision other)
    {

    }
}
