using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_Stage5 : OpenDoor
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO : 게임매니저에서 가져오도록 후처리
            if (FindAnyObjectByType<StageFifth>().yejins_Phone.takeOnPhone)
            {
                SceneLoadManager.Instance.StageLoad(StageNumber.Happy_Ending);
            }
            else
            {
                SceneLoadManager.Instance.StageLoad(StageNumber.Bad_Ending);
            }
        }
    }
}
