using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private void Awake()
    {
        newGameBTN.onClick.AddListener(NewGameClick);
        stageSelectBTN.onClick.AddListener(StageSelectClick);
        exitGameBTN.onClick.AddListener(ExitGameClick);
        exitYes.onClick.AddListener(ExitYesClick);
        exitNo.onClick.AddListener(ExitNoClick);
        stageSelectExitBTN.onClick.AddListener(StageSelectExitClick);
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
}
