using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] tile;

    private sbyte hitCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Hammer>(out Hammer hammer))
        {
            Debug.Log("hi");
            if (hammer.m_Speed > 50 && hitCount <= 1)
            {
                Debug.Log("hit");
                tile[hitCount].SetActive(false);
                hitCount++;
                tile[hitCount].SetActive(true);
            }

            if (hitCount == 2)
            {
                tile[hitCount + 1].SetActive(true);
            }
        }
    }
}
