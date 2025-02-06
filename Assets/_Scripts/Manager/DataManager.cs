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
    private string fileName = "JSON_SaveFile";
    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }
    public void SaveData(int stageNumber)
    {
        dataTable.currentStage = stageNumber;
        if (stageNumber != 0)
        {
            dataTable.isStageEnter[stageNumber - 1] = true;
        }
        string saveFile = JsonUtility.ToJson(dataTable);
        File.WriteAllText(SaveFilePath(), saveFile);
    }
    public void LoadData()
    {
        string loadFile;
        try
        {
            loadFile = File.ReadAllText(SaveFilePath());
        }
        catch
        {
            string saveFile = JsonUtility.ToJson(dataTable);
            File.WriteAllText(SaveFilePath(), saveFile);
        }
        finally
        {
            loadFile = File.ReadAllText(SaveFilePath());
        }
        dataTable = JsonUtility.FromJson<DataTable>(loadFile);
    }

    private string SaveFilePath()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, fileName);
        return path;
#elif UNITY_STANDALONE 
        // 빌드된 게임에서는 persistentDataPath 사용
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
#endif
    }
}