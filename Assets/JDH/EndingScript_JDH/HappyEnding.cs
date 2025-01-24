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

        //Ending 대사 출력
        yield return null;
        FirstEndingScript.gameObject.SetActive(true);
        yield return new WaitUntil(() => FirstEndingScript.isDialogsEnd);
        LastEndingScript.gameObject.SetActive(true);
        yield return new WaitUntil(() => LastEndingScript.isDialogsEnd);
        //Title 로고 출력
        EndingTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        //FadeOut
        StartCoroutine(fadeInOutObj.FadeOut());
        yield return new WaitForSeconds(3f);

        //MainTitleScene으로 이동
        SceneLoadManager.Instance.StageLoad(StageNumber.Title);
    }

}
