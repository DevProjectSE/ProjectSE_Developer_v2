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
            if (isStageEnter)
            {
                stageBTN[i].interactable = true;
            }
            else
            {
                break;
            }
            i++;
        }
        if (SceneManager.GetActiveScene().name != "TitleScene")
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
        //TODO : 겜매가 있어야 작동하는데, 01/21기준 겜매 싱글턴 작업 미완료
        GameManager.Instance.Player.
        GetComponentInChildren<CustomPlayerController>().UIOpen();
        titlePanel.SetActive(true);
        r_SecondaryKey.action.performed += MenuCloseAction;
    }
    private void MenuCloseAction(InputAction.CallbackContext context)
    {
        GameManager.Instance.Player.GetComponentInChildren<CustomPlayerController>().UIClose();
        titlePanel.SetActive(false);
        r_SecondaryKey.action.performed -= MenuCloseAction;
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
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage1);
        stagePanel.SetActive(false);
    }
    private void Stage2_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage2);
        stagePanel.SetActive(false);
    }
    private void Stage3_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage3);
        stagePanel.SetActive(false);
    }
    private void Stage4_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage4);
        stagePanel.SetActive(false);
    }
    private void Stage5_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage5);
        stagePanel.SetActive(false);
    }
    private void Stage6_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.BadEnding);
        stagePanel.SetActive(false);
    }
    private void Stage7_LoadBTNClick()
    {
        SceneLoadManager.Instance.StageLoad(StageNumber.HappyEnding);
        stagePanel.SetActive(false);
    }
}
