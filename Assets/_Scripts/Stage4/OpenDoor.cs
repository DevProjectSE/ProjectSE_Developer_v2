using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider.enabled = false;
    }
    private void Start()
    {
        StartCoroutine(OpenningDoor());
        boxCollider.enabled = true;
    }
    private IEnumerator OpenningDoor()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SaveLoadManager.Instance.StageLoad(StageNumber.Stage5);
    }
}
