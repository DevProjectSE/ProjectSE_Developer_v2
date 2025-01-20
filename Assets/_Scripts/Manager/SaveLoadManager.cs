using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageNumber
{
    Stage1 = 1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
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

}
