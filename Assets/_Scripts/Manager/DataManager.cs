using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Assertions.Must;
[Serializable]
public class DataTable
{
    // [Header("스테이지 클리어 여부")]
    // [Tooltip("스테이지1 클리어 여부")] public bool isStage1Clear;
    // [Tooltip("스테이지2 클리어 여부")] public bool isStage2Clear;
    // [Tooltip("스테이지3 클리어 여부")] public bool isStage3Clear;
    // [Tooltip("스테이지4 클리어 여부")] public bool isStage4Clear;
    // [Tooltip("스테이지5 클리어 여부")] public bool isStage5Clear;
    // [Tooltip("스테이지6 클리어 여부")] public bool isStage6Clear;
    // [Tooltip("스테이지7 클리어 여부")] public bool isStage7Clear;

    // [Header("엔딩 분기점")]
    // [SerializeField, Tooltip("해피엔딩")] public bool isHappyEnding;
    // [SerializeField, Tooltip("배드엔딩")] public bool isBadEnding;
    public List<bool> isStageEnter;

    public int currentStage;
}

public class DataManager : SingletonManager<DataManager>
{
    public DataTable dataTable = new DataTable();
    private string path;
    private string fileName = "save";
    protected override void Awake()
    {
        base.Awake();
        path = Application.persistentDataPath + "/";
    }
    public void SaveData(StageNumber stageNumber)
    {
        Debug.Log("SaveDataEnter");
        switch (stageNumber)
        {
            case StageNumber.Stage1:
                Debug.Log("Stage1");
                dataTable.currentStage = 1;
                dataTable.isStageEnter[0] = true;
                break;
            case StageNumber.Stage2:
                dataTable.currentStage = 2;
                dataTable.isStageEnter[1] = true;
                break;
            case StageNumber.Stage3:
                dataTable.currentStage = 3;
                dataTable.isStageEnter[2] = true;
                break;
            case StageNumber.Stage4:
                dataTable.currentStage = 4;
                dataTable.isStageEnter[3] = true;
                break;
            case StageNumber.Stage5:
                dataTable.currentStage = 5;
                dataTable.isStageEnter[4] = true;
                break;
            case StageNumber.BadEnding:
                dataTable.currentStage = 6;
                dataTable.isStageEnter[5] = true;
                break;
            case StageNumber.HappyEnding:
                dataTable.currentStage = 7;
                dataTable.isStageEnter[6] = true;
                break;
        }
        Debug.Log("Save");
        Debug.Log($"Path : {path + fileName}");
        string saveFile = JsonUtility.ToJson(dataTable);
        Debug.Log($"Json : {saveFile}");
        File.WriteAllText(path + fileName, saveFile);
        Debug.Log("SaveComplete");
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