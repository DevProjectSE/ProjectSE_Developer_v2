using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2LastDoor : OpenDoor
{
    protected override IEnumerator OpenningDoor()
    {
        float t = 0.01f;
        while (true)
        {
            transform.localPosition = new Vector3(transform.localPosition.z + t, transform.localPosition.y, transform.localPosition.z);
            t += 0.01f;
            if (t > 3)
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoadManager.Instance.StageLoad(StageNumber.Stage3);
        }
    }

}
