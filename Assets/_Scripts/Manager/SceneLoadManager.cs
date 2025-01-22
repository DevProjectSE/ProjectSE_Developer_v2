using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageNumber
{
    Stage1 = 1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    HappyEnding,
    BadEnding
}

public class SceneLoadManager : SingletonManager<SceneLoadManager>
{

    protected override void Awake()
    {
        base.Awake();
    }
    public void StageLoad(StageNumber stageNumber)
    {
        switch (stageNumber)
        {
            case StageNumber.Stage1:
                LoadingScene.LoadScene("Stage1_JDH");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.Stage2:
                LoadingScene.LoadScene("Stage2_AJH");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.Stage3:
                LoadingScene.LoadScene("Stage3_AJH");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.Stage4:
                LoadingScene.LoadScene("Stage4_Complete");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.Stage5:
                LoadingScene.LoadScene("Stage5_Complete");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.BadEnding:
                LoadingScene.LoadScene("Stage6_Bad");
                DataManager.Instance.SaveData(stageNumber);
                break;
            case StageNumber.HappyEnding:
                LoadingScene.LoadScene("Stage6_Happy");
                DataManager.Instance.SaveData(stageNumber);
                break;
        }
    }

}