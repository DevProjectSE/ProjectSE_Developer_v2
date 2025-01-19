using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum StageNumber
{
    Stage1 = 1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

public class DataTable
{
    public bool isStage_1_Clear;
    public bool isStage_2_Clear;
    public bool isStage_3_Clear;
    public bool isStage_5_Clear;

    public Vector3 stage_1_StartPos;
    public Vector3 stage_2_StartPos;
    public Vector3 stage_3_StartPos;
    public Vector3 stage_4_StartPos;
    public Vector3 stage_5_StartPos;

    public int currentStage;
}

public class DataManager : MonoBehaviour
{
    public DataTable dataTable;
    StageNumber stageNumber;

    public void StageLoad()
    {
        switch (stageNumber)
        {
            case StageNumber.Stage1:

                break;
            case StageNumber.Stage2:
                break;
            case StageNumber.Stage3:
                break;
            case StageNumber.Stage4:
                break;
            case StageNumber.Stage5:
                break;

        }
    }

    public void Stage1()
    {
        GameManager.Instance.Player.transform.position = dataTable.stage_1_StartPos;
    }

    public void Stage2()
    {
        GameManager.Instance.Player.transform.position = dataTable.stage_2_StartPos;
    }

    public void Stage3()
    {
        GameManager.Instance.Player.transform.position = dataTable.stage_3_StartPos;
    }

    public void Stage4()
    {
        GameManager.Instance.Player.transform.position = dataTable.stage_4_StartPos;
    }
    public void Stage5()
    {
        GameManager.Instance.Player.transform.position = dataTable.stage_5_StartPos;
    }
}