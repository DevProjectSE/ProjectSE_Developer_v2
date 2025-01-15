using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialCoke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            FindObjectOfType<StageManager>().throwCokecan = true;
            Debug.Log("해당 오브젝트는 더이상 잡을 수 없습니다.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        FindObjectOfType<StageManager>().throwCokecan = false;
        Debug.Log("해당 오브젝트가 상호작용이 가능해졌습니다.");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
