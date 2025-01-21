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
    public List<bool> isStageEnter;
    public int currentStage;
    public List<Material> bookMats;
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
        try
        {
            LoadData();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
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

    public void LoadData()
    {
        string loadFile = File.ReadAllText(path + fileName);
        dataTable = JsonUtility.FromJson<DataTable>(loadFile);
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