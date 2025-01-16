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
        else if(other.CompareTag("Hand"))
        {
            //손에 닿았을 경우
            FindObjectOfType<StageManager>().isTouchDrink = true;
            Debug.Log("음료가 손에 닿았습니다.");
        }
        else if(other.CompareTag("MainCamera"))
        {
            //카메라의 콜라이더와 접촉 시 마시는 효과음과 함께 활성화 된 콜라 마시는 UI 비활성화
            FindObjectOfType<StageManager>().isCokeDrink = true;
            //효과음 재생
            Debug.Log("음료를 마셨습니다.");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            FindObjectOfType<StageManager>().throwCokecan = false;
            Debug.Log("해당 오브젝트가 상호작용이 가능해졌습니다.");
        }
        else if(other.CompareTag("Hand"))
        {
            //손에서 떨어졌을 경우
            FindObjectOfType<StageManager>().isTouchDrink = false;
            Debug.Log("음료가 손에서 떨어졌습니다.");
        }
        
    }
}
