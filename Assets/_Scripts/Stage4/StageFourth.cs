using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFourth : MonoBehaviour
{
    private bool isMirrorClear { get; set; }
    private bool isTrashCanClear { get; set; }
    private bool isLockerClear { get; set; }
    private bool isWallBreakClear { get; set; }
    public List<Stain> stains;
    public List<GameObject> stageFourtItem;

    public TrashCan_Active trashCan;

    private void Awake()
    {
        if (trashCan == null)
        {
            trashCan = GetComponentInChildren<TrashCan_Active>();
        }
        foreach (GameObject g in stageFourtItem)
        {
            g.SetActive(false);
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
        //ToDo : 유리 교체
    }
    public void TrashCanClear()
    {
        isTrashCanClear = true;
        stageFourtItem[0].gameObject.SetActive(true);
    }
    public void LockerClear()
    {
        isLockerClear = true;
        stageFourtItem[1].gameObject.SetActive(true);
    }

    public void WallBreakClear()
    {
        isWallBreakClear = true;
        stageFourtItem[2].gameObject.SetActive(true);
    }

}
