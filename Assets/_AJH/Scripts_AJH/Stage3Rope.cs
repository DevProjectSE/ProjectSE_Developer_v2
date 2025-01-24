using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Rope : MonoBehaviour
{
    public GameObject knife;

    void OnTriggerEnter(Collider other)
    {
        //print("충돌함" + other.gameObject.name);   

        if (other.gameObject.CompareTag("Knife"))
        {
            //print("충돌함2");
            Destroy(gameObject);
            Stage3ItemManager.Instance.ropeNum--;
            print(Stage3ItemManager.Instance.ropeNum);
            if (Stage3ItemManager.Instance.ropeNum == 0)
            {
                if (Stage3ItemManager.Instance.stageState == 6)
                {
                    Stage3StateManager.Instance.robot.SetActive(true);
                    Stage3StateManager.Instance.cleanLockDoor.enabled = true;
                    Stage3ItemManager.Instance.stageState++;
                }
            }
        }
    }

}
