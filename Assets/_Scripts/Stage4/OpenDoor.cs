using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private IEnumerator Start()
    {
        float t = -3;
        while (true)
        {
            transform.position = new Vector3(Mathf.Lerp(0, t, 0.1f), 0, 0);
            if (transform.position.x < -2.9f) yield break;
        }
    }

}
