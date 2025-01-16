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
            Debug.Log("�ش� ������Ʈ�� ���̻� ���� �� �����ϴ�.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        FindObjectOfType<StageManager>().throwCokecan = false;
        Debug.Log("�ش� ������Ʈ�� ��ȣ�ۿ��� �����������ϴ�.");
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
