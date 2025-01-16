using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//플레이어 화면에 구현되는 UI, 파괴되지않는다.
public class UIManager : MonoBehaviour
{
    [Header("UI GameObject")]
    public FadeInOut fadeInOutObj;

    public GameObject tutorialUI;
    public GameObject playerDialog;

    public GameObject mainQuestUiObj;
    public GameObject miniMainQuestUiObj;
    public GameObject controllerTutoObj;

    [Header("Menu UI")]
    public GameObject menuUiObj;

    [Header("Player Dialogue")]
    public TextMeshProUGUI Name;
    public TextMeshProUGUI dialog;

    [Header("UI Image")]
    public Image controllerQuestImg;

    [Header("UI Text")]
    public TextMeshProUGUI questText;
    public TextMeshProUGUI controllerQuestText;
    public TextMeshProUGUI miniQuestText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //튜토리얼 UI 변경
    public void ChangeTutorialText(TextMeshProUGUI text, string DescText)
    {
        text.text = DescText;
    }
#if true

#endif
    //튜토리얼 UI 이미지까지 변경
    public void ChanageAllTutorialUI(TextMeshProUGUI text, Image image, string DescText, Sprite changeImg)
    {
        image.sprite = changeImg;
        text.text = DescText;
    }

}
