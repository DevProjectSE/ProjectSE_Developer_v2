using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    private StageFourth stageFourth;
    public MeshRenderer m_MeshRenderer { get; private set; }
    private int count = 0;

    private void Awake()
    {
        if (stageFourth == null)
        {
            stageFourth = GetComponentInParent<StageFourth>();
        }
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
            if (m_MeshRenderer.material.color.a <= 0)
            {
                stageFourth.StainClear();
                gameObject.SetActive(false);
            }
        }
    }
}
