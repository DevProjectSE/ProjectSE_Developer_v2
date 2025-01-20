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
            Debug.Log("플레이어가 튜토리얼 시작 지점에 도착했습니다.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<TutorialManager>().isTutorialArea = false;
            Debug.Log("플레이어가 튜토리얼 시작 지점에서 나갔습니다.");
        }
    }

}
