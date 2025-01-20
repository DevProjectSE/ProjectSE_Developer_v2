using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DataTable
{
    [Header("스테이지 클리어 여부")]
    [Tooltip("스테이지1 클리어 여부")] public bool isStage1Clear;
    [Tooltip("스테이지2 클리어 여부")] public bool isStage2Clear;
    [Tooltip("스테이지3 클리어 여부")] public bool isStage3Clear;
    [Tooltip("스테이지4 클리어 여부")] public bool isStage4Clear;
    [Tooltip("스테이지5 클리어 여부")] public bool isStage5Clear;

    [Header("엔딩 분기점")]
    [SerializeField, Tooltip("해피엔딩")] public bool isHappyEnding;
    [SerializeField, Tooltip("배드엔딩")] public bool isBadEnding;

    public int currentStage;
}

public class DataManager : SingletonManager<DataManager>
{
    public DataTable dataTable;

    protected override void Awake()
    {
        base.Awake();
    }


    public void Stage1()
    {

    }

    public void Stage2()
    {

    }

    public void Stage3()
    {

    }

    public void Stage4()
    {

    }
    public void Stage5()
    {
    }
}