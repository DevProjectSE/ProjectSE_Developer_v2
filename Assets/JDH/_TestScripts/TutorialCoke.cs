using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialCoke : MonoBehaviour
{
    public void GrabCoke()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isTouchDrink = true;
        Debug.Log("���ᰡ �տ� ��ҽ��ϴ�.");
    }
    public void CancelGrabCoke()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isTouchDrink = false;
        Debug.Log("���ᰡ �տ� ���� ��");
    }
    private void OnTriggerEnter(Collider other)
    {
        // I Am IRON MAN;
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            FindObjectOfType<TutorialManager>().throwCokecan = true;
            Debug.Log("�ش� ������Ʈ�� ���̻� ���� �� �����ϴ�.");
        }
        else if(other.CompareTag("MainCamera"))
        {
            //ī�޶��� �ݶ��̴��� ���� �� ���ô� ȿ������ �Բ� Ȱ��ȭ �� �ݶ� ���ô� UI ��Ȱ��ȭ
            FindObjectOfType<TutorialManager>().isCokeDrink = true;
            //ȿ���� ���
            Debug.Log("���Ḧ ���̽��ϴ�.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            FindObjectOfType<TutorialManager>().throwCokecan = false;
            Debug.Log("�ش� ������Ʈ�� ��ȣ�ۿ��� �����������ϴ�.");
        }

        
    }
}
