using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] tile;

    private sbyte hitCount = 0;

    [Tooltip("도달해야하는 값")]
    public float reachingSpeed;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Hammer>(out Hammer hammer))
        {
            if (hammer.rb.velocity.magnitude > reachingSpeed && hitCount <= 1)
            {
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
