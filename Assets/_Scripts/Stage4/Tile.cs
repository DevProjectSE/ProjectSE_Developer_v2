using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public StageFourth stageFourth;
    public GameObject[] tile;
    private sbyte hitCount = 0;
    [Tooltip("도달해야하는 값")]
    public float reachingSpeed;

    private float tempTime = 0;
    private bool cheak = false;

    private void Awake()
    {
        if (stageFourth == null)
        {
            stageFourth = GetComponentInParent<StageFourth>();
        }
    }
    private void FixedUpdate()
    {
        if (cheak)
        {
            tempTime += Time.deltaTime;
            if (tempTime > 1f) cheak = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Hammer>(out Hammer hammer))
        {
            if (hammer.m_Rb.velocity.magnitude > reachingSpeed && hitCount == 0)
            {
                ActivateObj();
                cheak = true;
                return;
            }
            if (hammer.m_Rb.velocity.magnitude > reachingSpeed && hitCount == 1 && tempTime > 1f)
            {
                ActivateObj();
                tile[hitCount + 1].SetActive(true);
                stageFourth.WallBreakClear();
                GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
    }

    private void ActivateObj()
    {
        tile[hitCount].SetActive(false);
        hitCount++;
        tile[hitCount].SetActive(true);
    }
}
