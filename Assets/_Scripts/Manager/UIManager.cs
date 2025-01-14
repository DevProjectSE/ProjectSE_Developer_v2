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

    [Header("Player Dialogue")]
    public TextMeshProUGUI Name;
    public TextMeshProUGUI dialog;

    [Header("UI Image")]
    public Image controllerQuestImg;

    [Header("UI Text")]
    public TextMeshProUGUI questText;
    public TextMeshProUGUI controllerQuestText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
