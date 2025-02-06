using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageNumber
{
    Title,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Happy_Ending,
    Bad_Ending
}

public class SceneLoadManager : SingletonManager<SceneLoadManager>
{

    protected override void Awake()
    {
        base.Awake();
    }
    public void StageLoad(StageNumber stageNumber)
    {
        LoadingScene.LoadScene(stageNumber.ToString());
        DataManager.Instance.SaveData((int)stageNumber);
    }
}