using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialPhone : MonoBehaviour
{
    public void GrabPhone()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isGetThePhone = true;
        Debug.Log("�ڵ����� ��ҽ��ϴ�.");
    }
    public void CancelGrabPhone()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isGetThePhone = false;
        Debug.Log("�ڵ����� ���ҽ��ϴ�.");
    }
    
}
