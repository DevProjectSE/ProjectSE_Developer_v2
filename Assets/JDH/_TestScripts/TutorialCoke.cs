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
        else if(other.CompareTag("Hand"))
        {
            //�տ� ����� ���
            FindObjectOfType<StageManager>().isTouchDrink = true;
            Debug.Log("���ᰡ �տ� ��ҽ��ϴ�.");
        }
        else if(other.CompareTag("MainCamera"))
        {
            //ī�޶��� �ݶ��̴��� ���� �� ���ô� ȿ������ �Բ� Ȱ��ȭ �� �ݶ� ���ô� UI ��Ȱ��ȭ
            FindObjectOfType<StageManager>().isCokeDrink = true;
            //ȿ���� ���
            Debug.Log("���Ḧ ���̽��ϴ�.");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            FindObjectOfType<StageManager>().throwCokecan = false;
            Debug.Log("�ش� ������Ʈ�� ��ȣ�ۿ��� �����������ϴ�.");
        }
        else if(other.CompareTag("Hand"))
        {
            //�տ��� �������� ���
            FindObjectOfType<StageManager>().isTouchDrink = false;
            Debug.Log("���ᰡ �տ��� ���������ϴ�.");
        }
        
    }
}
