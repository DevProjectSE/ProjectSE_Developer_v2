using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HappyEnding : MonoBehaviour
{
    public DialogSystem FirstEndingScript;
    public DialogSystem LastEndingScript;

    public TextMeshProUGUI EndingTitle;

    public FadeInOut fadeInOutObj;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameManager.Instance.Player.GetComponentInChildren<CustomPlayerController>().CtrlRelease();

        //Ending ��� ���
        yield return null;
        FirstEndingScript.gameObject.SetActive(true);
        yield return new WaitUntil(() => FirstEndingScript.isDialogsEnd);
        LastEndingScript.gameObject.SetActive(true);
        yield return new WaitUntil(() => LastEndingScript.isDialogsEnd);
        //Title �ΰ� ���
        EndingTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        //FadeOut
        StartCoroutine(fadeInOutObj.FadeOut());
        yield return new WaitForSeconds(3f);

        //MainTitleScene���� �̵�
        SceneLoadManager.Instance.StageLoad(StageNumber.Title);
    }

}
