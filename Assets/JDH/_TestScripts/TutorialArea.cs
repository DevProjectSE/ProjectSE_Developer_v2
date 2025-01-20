using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<TutorialManager>().isTutorialArea = true;
            Debug.Log("�÷��̾ Ʃ�丮�� ���� ������ �����߽��ϴ�.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<TutorialManager>().isTutorialArea = false;
            Debug.Log("�÷��̾ Ʃ�丮�� ���� �������� �������ϴ�.");
        }
    }

}
