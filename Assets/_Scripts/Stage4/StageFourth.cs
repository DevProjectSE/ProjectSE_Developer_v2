using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class StageFourth : MonoBehaviour
{
    private bool isMirrorClear { get; set; }
    private bool isTrashCanClear { get; set; }
    private bool isLockerClear { get; set; }
    private bool isWallBreakClear { get; set; }
    public List<Stain> stains;
    public List<GameObject> stageFourtItem;
    public TrashCan_Active trashCan;
    public XRKnob toiletDoorKnob;
    //TODO : 리팩할 때 참고 : 로봇 조합 시 열리는 문
    public OpenDoor openDoor;
    private void Awake()
    {
        if (trashCan == null)
        {
            trashCan = GetComponentInChildren<TrashCan_Active>();
        }
        for (int i = 1; i < stageFourtItem.Count; i++)
        {
            stageFourtItem[i].SetActive(false);
        }
    }

    private void Start()
    {
        trashCan.GrabActivate(false);
    }

    public void StainClear()
    {
        foreach (Stain s in stains)
        {
            if (s.m_MeshRenderer.material.color.a > 0)
            {
                return;
            }
        }
        isMirrorClear = true;
        stains.Clear();
        trashCan.GrabActivate(true);
        stageFourtItem[0].layer = LayerMask.NameToLayer("Object");
        stageFourtItem[0].GetComponent<XRGrabInteractable>().enabled = false;
        //ToDo : 유리 교체
    }
    public void TrashCanClear()
    {
        isTrashCanClear = true;
        stageFourtItem[1].gameObject.SetActive(true);
    }
    public void LockerClear()
    {
        isLockerClear = true;
        stageFourtItem[2].gameObject.SetActive(true);
        toiletDoorKnob.enabled = true;
    }

    public void WallBreakClear()
    {
        stageFourtItem[2].layer = LayerMask.NameToLayer("Object");
        stageFourtItem[2].GetComponent<XRGrabInteractable>().enabled = false;
        isWallBreakClear = true;
        stageFourtItem[3].gameObject.SetActive(true);
    }

}
