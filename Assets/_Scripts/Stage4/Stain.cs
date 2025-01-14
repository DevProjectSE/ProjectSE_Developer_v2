using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{

    private MeshRenderer m_MeshRenderer;
    private int count = 0;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Handmop"))
        {
            count++;
            if (count > 3)
            {
                m_MeshRenderer.material.color -= new Color(0, 0, 0, 0.25f);
                count = 0;
            }
        }
    }
}
