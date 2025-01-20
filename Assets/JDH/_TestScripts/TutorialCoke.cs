using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialCoke : MonoBehaviour
{
    public void GrabCoke()
    {
        //손에 닿았을 경우 =>인식 안됨.
        FindObjectOfType<TutorialManager>().isTouchDrink = true;
        Debug.Log("음료가 손에 닿았습니다.");
    }
    public void CancelGrabCoke()
    {
        //손에 닿았을 경우 =>인식 안됨.
        FindObjectOfType<TutorialManager>().isTouchDrink = false;
        Debug.Log("음료가 손에 없을 때");
    }
    private void OnTriggerEnter(Collider other)
    {
        // I Am IRON MAN;
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            FindObjectOfType<TutorialManager>().throwCokecan = true;
            Debug.Log("해당 오브젝트는 더이상 잡을 수 없습니다.");
        }
        else if(other.CompareTag("MainCamera"))
        {
            //카메라의 콜라이더와 접촉 시 마시는 효과음과 함께 활성화 된 콜라 마시는 UI 비활성화
            FindObjectOfType<TutorialManager>().isCokeDrink = true;
            //효과음 재생
            Debug.Log("음료를 마셨습니다.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TrashCan"))
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            FindObjectOfType<TutorialManager>().throwCokecan = false;
            Debug.Log("해당 오브젝트가 상호작용이 가능해졌습니다.");
        }

        
    }
}
