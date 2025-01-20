using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialPhone : MonoBehaviour
{
    public void GrabPhone()
    {
        //손에 닿았을 경우 =>인식 안됨.
        FindObjectOfType<TutorialManager>().isGetThePhone = true;
        Debug.Log("핸드폰을 잡았습니다.");
    }
    public void CancelGrabPhone()
    {
        //손에 닿았을 경우 =>인식 안됨.
        FindObjectOfType<TutorialManager>().isGetThePhone = false;
        Debug.Log("핸드폰을 놓았습니다.");
    }
    
}
