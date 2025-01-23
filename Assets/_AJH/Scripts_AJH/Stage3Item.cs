using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Stage3Item : MonoBehaviour
{
 

    protected virtual void OnTriggerEnter(Collider other)
    {
   
    }

    public void GetFlash()
    {
        if (Stage3StateManager.Instance.stage3Step == 2)
        {
            Stage3StateManager.Instance.stage3Step = 3;
            Stage3StateManager.Instance.isGetLight = true;
        }
    }

    public void GetDiary()
    {
        if (Stage3StateManager.Instance.stage3Step == 4)
        {
            Stage3StateManager.Instance.stage3Step = 5;
            Stage3StateManager.Instance.isGetDiary = true;
        }
    }

    public void GetBook()
    {
        if (Stage3StateManager.Instance.stage3Step == 6)
        {
            Stage3StateManager.Instance.stage3Step = 7;
            Stage3StateManager.Instance.isGetBook = true;
        }
    }
    public void GetHomework()
    {
        if (Stage3StateManager.Instance.stage3Step == 8)
        {
            Stage3StateManager.Instance.stage3Step = 9;
            Stage3StateManager.Instance.isGetHomework=true;
        }
    }

    public void GetBackpack()
    {
        if (Stage3StateManager.Instance.stage3Step == 10)
        {
            Stage3StateManager.Instance.stage3Step = 11;
            Stage3StateManager.Instance.isGetBagpack = true;
        }
    }
    public void Setting()
    {
        Stage3StateManager.Instance.setNum++;
        if (Stage3StateManager.Instance.stage3Step == 11 && Stage3StateManager.Instance.setNum == 3)
        {
            Stage3StateManager.Instance.stage3Step = 12;
            Stage3StateManager.Instance.isSettingHaYunsObject = true;
        }
    }


    public void GetRobot()
    {
        if (Stage3StateManager.Instance.stage3Step == 13)
        {
            Stage3StateManager.Instance.stage3Step = 14;
            Stage3StateManager.Instance.isGetRobot = true;
        }
    }
}
