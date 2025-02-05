using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Title : MonoBehaviour
{
    [SerializeField]
    private InputActionReference menuAction;
    [SerializeField]
    private InputActionReference r_SecondaryKey;
    public GameObject titlePanel;
    public GameObject exitPanel;
    public GameObject stagePanel;
    public Button newGameBTN;
    public Button stageSelectBTN;
    public Button stageSelectExitBTN;
    public Button exitGameBTN;
    public Button exitYes;
    public Button exitNo;

    #region 스테이지 로드버튼
    public List<Button> stageBTN;
    #endregion

    private bool menuOpenClose = false;

    private void Awake()
    {
        newGameBTN.onClick.AddListener(NewGameClick);
        stageSelectBTN.onClick.AddListener(StageSelectClick);
        exitGameBTN.onClick.AddListener(ExitGameClick);
        exitYes.onClick.AddListener(ExitYesClick);
        exitNo.onClick.AddListener(ExitNoClick);
        stageSelectExitBTN.onClick.AddListener(StageSelectExitClick);
        stageBTN[0].onClick.AddListener(Stage1_LoadBTNClick);
        stageBTN[1].onClick.AddListener(Stage2_LoadBTNClick);
        stageBTN[2].onClick.AddListener(Stage3_LoadBTNClick);
        stageBTN[3].onClick.AddListener(Stage4_LoadBTNClick);
        stageBTN[4].onClick.AddListener(Stage5_LoadBTNClick);
        stageBTN[5].onClick.AddListener(Stage6_LoadBTNClick);
        stageBTN[6].onClick.AddListener(Stage7_LoadBTNClick);
        foreach (Button button in stageBTN)
        {
            button.interactable = false;
        }

    }
    private void Start()
    {
        int i = 0;
        foreach (bool isStageEnter in DataManager.Instance.dataTable.isStageEnter)
        {
            if (isStageEnter) stageBTN[i].interactable = true;
            else break;
            i++;
        }
        if (SceneManager.GetActiveScene().name != "Title_Complete")
        {
            titlePanel.SetActive(false);
        }
    }
    private void OnEnable()
    {
        menuAction.action.performed += MenuAction;
    }
    private void OnDisable()
    {
        menuAction.action.performed -= MenuAction;
    }
    private void MenuAction(InputAction.CallbackContext context)
    {
        if (menuOpenClose == false)
        {
            menuOpenClose = true;
            GetComponentInParent<CustomPlayerController>().UIOpen();
            titlePanel.SetActive(menuOpenClose);
        }
        else
        {
            menuOpenClose = false;
            GetComponentInParent<CustomPlayerController>().UIClose();
            titlePanel.SetActive(menuOpenClose);
        }
    }
    private void NewGameClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage1);
        titlePanel.SetActive(false);
    }
    private void StageSelectClick()
    {
        stagePanel.SetActive(true);
        titlePanel.SetActive(false);
    }
    private void StageSelectExitClick()
    {
        titlePanel.SetActive(true);
        stagePanel.SetActive(false);
    }
    private void ExitGameClick()
    {
        exitPanel.SetActive(true);
        titlePanel.SetActive(false);
    }
    private void ExitYesClick()
    {
        Application.Quit();
    }
    private void ExitNoClick()
    {
        titlePanel.SetActive(true);
        exitPanel.SetActive(false);
    }
    private void Stage1_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage2_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage3_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage4_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage5_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage6_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void Stage7_LoadBTNClick()
    {
        StageSelect(StageNumber.HappyEnding);
    }
    private void StageSelect(StageNumber stageNumber)
    {
        SceneLoadManager.Instance.StageLoad(stageNumber);
        stagePanel.SetActive(false);
    }
}
