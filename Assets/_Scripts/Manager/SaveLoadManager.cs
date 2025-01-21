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

public class SaveLoadManager : SingletonManager<SaveLoadManager>
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
                DataManager.Instance.dataTable.currentStage = 1;
                break;
            case StageNumber.Stage2:
                LoadingScene.LoadScene("Stage2_AJH");
                DataManager.Instance.dataTable.currentStage = 2;
                break;
            case StageNumber.Stage3:
                LoadingScene.LoadScene("Stage3_AJH");
                DataManager.Instance.dataTable.currentStage = 3;
                break;
            case StageNumber.Stage4:
                LoadingScene.LoadScene("KimChanYoung_Stage4");
                DataManager.Instance.dataTable.currentStage = 4;
                break;
            case StageNumber.Stage5:
                LoadingScene.LoadScene("KimChanYoung_Stage5");
                DataManager.Instance.dataTable.currentStage = 5;
                break;

        }
    }

}
