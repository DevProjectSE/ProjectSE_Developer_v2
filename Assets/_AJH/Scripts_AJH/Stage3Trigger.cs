using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Ãæµ¹");
            if (Stage3StateManager.Instance.stage3Step == 0 || Stage3StateManager.Instance.stage3Step == 1 ||
                Stage3StateManager.Instance.stage3Step == 3 || Stage3StateManager.Instance.stage3Step == 12)
            {
                Stage3StateManager.Instance.stage3Step++;
            }
        }
    }
    public void NextStageStep()
    {
        if (Stage3StateManager.Instance.stage3Step == 0 || Stage3StateManager.Instance.stage3Step == 1 ||
                Stage3StateManager.Instance.stage3Step == 3 || Stage3StateManager.Instance.stage3Step == 12)
        {
            Stage3StateManager.Instance.stage3Step++;
        }
    }
}