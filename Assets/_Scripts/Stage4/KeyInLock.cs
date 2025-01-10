using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class KeyInLock : MonoBehaviour
{

    private XRKnob knob;
    public Transform openHint;
    public Transform upper_Part_LOD0;
    public List<BoxCollider> upper_Part_collsList = new();

    private bool isActived = false;

    private void Awake()
    {
        knob = GetComponent<XRKnob>();
    }

    public void OnActive()
    {   //이미 풀렸으면 이후에는 OnActive 호출 안하도록 처리
        if (isActived) return;
        //열쇠를 끝까지 돌렸는지 확인
        if (knob.value <= 0.01f)
        {
            upper_Part_LOD0.transform.position = openHint.position;
            foreach (BoxCollider coll in upper_Part_collsList)
            {
                coll.enabled = false;
                isActived = true;
            }
        }
    }
}
