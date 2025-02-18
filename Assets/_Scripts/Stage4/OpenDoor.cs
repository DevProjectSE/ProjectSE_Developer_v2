using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public BoxCollider boxCollider;
    protected virtual void Awake()
    {
        boxCollider.enabled = false;
    }
    protected virtual void Start()
    {
        StartCoroutine(OpenningDoor());
        boxCollider.enabled = true;
    }
    protected virtual IEnumerator OpenningDoor()
    {
        float t = 0.01f;
        while (true)
        {
            transform.localPosition = new Vector3(transform.localPosition.z - t, transform.localPosition.y, transform.localPosition.z);
            t += 0.01f;
            if (t > 3)
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneLoadManager.Instance.StageLoad(StageNumber.Stage5);
    }
}
