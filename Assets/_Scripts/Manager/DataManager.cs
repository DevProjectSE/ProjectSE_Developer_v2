using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Assertions.Must;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Unity.VisualScripting;
[Serializable]
public class DataTable
{
    public List<bool> isStageEnter;
    public int currentStage;
    public List<float> diaryPage_Dissolve;
}

public class DataManager : SingletonManager<DataManager>
{

    public DataTable dataTable = new DataTable();
    private string path;
    private string fileName = "JSON_SaveFile";
    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, fileName);
#else
        // 빌드된 게임에서는 persistentDataPath 사용
        path = Path.Combine(Application.persistentDataPath, "SaveData");
#endif
        LoadData();
    }
    public void SaveData(StageNumber stageNumber)
    {
        switch (stageNumber)
        {
            case StageNumber.Title:
                dataTable.currentStage = 0;
                break;
            case StageNumber.Stage1:
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
        string saveFile = JsonUtility.ToJson(dataTable);
        File.WriteAllText(path, saveFile);
    }

    public void LoadData()
    {
        string loadFile;
        try
        {
            loadFile = File.ReadAllText(path);
        }
        catch
        {
            string saveFile = JsonUtility.ToJson(dataTable);
            File.WriteAllText(path, saveFile);
        }
        finally
        {
            loadFile = File.ReadAllText(path);
            dataTable = JsonUtility.FromJson<DataTable>(loadFile);
        }
    }

}