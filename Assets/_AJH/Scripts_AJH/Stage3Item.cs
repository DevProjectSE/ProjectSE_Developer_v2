using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Stage3Item : MonoBehaviour
{
   

    public void GetKnife()
    {

    }

    public void GetDiary()
    {
        if (Stage3StateManager.Instance.stage3Step == 4)
        {
            Stage3StateManager.Instance.stage3Step = 5;
        }
    }

    public void GetFlash()
    {
        if (Stage3StateManager.Instance.stage3Step == 2)
        {
            Stage3StateManager.Instance.stage3Step = 3;
        }
    }

    public void GetBook()
    {
        if (Stage3StateManager.Instance.stage3Step == 6)
        {
            Stage3StateManager.Instance.stage3Step = 7;
        }
    }

    public void GetBackpack()
    {
        if (Stage3StateManager.Instance.stage3Step == 10)
        {
            Stage3StateManager.Instance.stage3Step = 11;
        }
    }

    public void GetHomework()
    {
        if (Stage3StateManager.Instance.stage3Step == 8)
        {
            Stage3StateManager.Instance.stage3Step = 9;
        }
    }

    public void GetRobot()
    {
        if (Stage3StateManager.Instance.stage3Step == 13)
        {
            Stage3StateManager.Instance.stage3Step = 14;
            
        }
    }
}
