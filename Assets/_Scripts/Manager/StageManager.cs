using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.OpenXR.Input;

public class StageManager : MonoBehaviour
{
    public enum STAGE { STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 };
    public STAGE stage;

    [Header("Stage1_DialogSystem")]
    public DialogSystem firstTutorialDialog;
    public DialogSystem secondTutorialDialog;
    [Header("Stage1_Tutorial_Sprites")]
    public Sprite viewTutorialImg;
    public Sprite movingTutorialImg;
    public Sprite gripTutorialImg;
    public Sprite objectActiveTutorialImg;
    public Sprite putTutorialImg;
    public Sprite sitdownTutorialImg;

    public InputActionReference rightJoystick;
    public InputActionReference leftJoystick;
    public InputActionReference leftGripButton;
    public InputActionReference rightGripButton;
    public InputActionReference rightPrimaryButton;

    [Header("Ʃ�丮�� ���� Ȯ�� �÷���")]
    private bool isSnapTurned = false; // ������ ���� ���� �÷���
    private bool isPlayerMoved = false; //�÷��̾� �̵� ���� �÷���

    public bool isTutorialArea = false; //Ʃ�丮�� ���� ���� ��ġ�� �����ϴ��� Ȯ���ϴ� �÷���
    public bool isSitdown = false;  //�ɾҴ��� Ȯ���ϴ� �÷���

    public bool isTouchDrink = false;    //�ݶ� �տ� ����ִ��� Ȯ���ϴ� �÷���
    private bool isGripButtonPress = false; //�׸� ��ư�� �������� Ȯ���ϴ� �÷���
    private bool isCanOpen = false; //�ݶ� ĵ�� ������ Ȯ���ϴ� �÷���
    public bool isCokeDrink = false;    //�ݶ� ���̴��� Ȯ���ϴ� �÷���
    public bool throwCokecan = false;   //������ ������ ���� �÷���

    public bool isInventoryOpen = false;    //�κ��丮 ���࿩�� Ȯ�� �÷���
    public bool isGetThePhone = false;  //��ȭ�� �޾Ҵ��� Ȯ���ϴ� �÷���

    [Header("��ƽ")]
    public XRBaseController leftXRController;
    public XRBaseController rightXRController;

    private void OnEnable()
    {
        rightJoystick.action.performed += RightHandSnapturn;
        leftJoystick.action.performed += LeftHandMove;

        leftGripButton.action.performed += GripButtonPressed;
        leftGripButton.action.canceled += GripButtonReleased;
        rightGripButton.action.performed += GripButtonPressed;
        rightGripButton.action.canceled += GripButtonReleased;
        rightPrimaryButton.action.performed += OpenCan;
    }
    private void OnDisable()
    {
        rightJoystick.action.performed -= RightHandSnapturn;
        leftJoystick.action.performed -= LeftHandMove;

        leftGripButton.action.performed -= GripButtonPressed;
        leftGripButton.action.canceled -= GripButtonReleased;
        rightGripButton.action.performed -= GripButtonPressed;
        rightGripButton.action.canceled -= GripButtonReleased;
        rightPrimaryButton.action.performed -= OpenCan;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        switch (stage)
        {
            case STAGE.STAGE1:
                //������Ʈ�� �� �Ⱦ �� �÷��̾��� ��ġ�� ī�޶� �̵��ϸ� ���� �÷��̾ ������ �� �ִ�.
                GameManager.Instance.Player.GetComponentInChildren<InputActionManager>().enabled = false;
                //��� ���� ��� ���� �÷��̾� ĳ���� ���� Ȱ��ȭ
                firstTutorialDialog.gameObject.SetActive(true);
                yield return new WaitUntil(() => firstTutorialDialog.isDialogsEnd == true);

                //����Ʈ ���� �ʱ�ȭ
                GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
                    "�ξ����� �̵��Ͽ� ������� ������� ���� ���ü���.");
                //����Ʈ �˸� UI Ȱ��ȭ 2�ʰ� Ȱ��ȭ ���� �ش� UI�� ��ҵǰ� ���� ��ܿ� ����
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
                yield return new WaitForSeconds(2f);
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
                GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);

                //��� �� UI �ʱ�ȭ
                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "���� ��Ʈ�ѷ��� ��ƽ�� �̿��Ͽ� ������ ��������.", viewTutorialImg);
                //�þ� ����Ű �˸� UI �����ʿ� ���
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

                //��Ʈ�ѷ��� ���� �þ� ������ ����Ǹ� �þ� �˸� UI ��Ȱ��ȭ
                // ������ ����� ������ ���
                yield return new WaitUntil(() => isSnapTurned);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

                // UI �̵� ��� �������� �ʱ�ȭ
                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "���� ��Ʈ�ѷ��� ��ƽ�� �̿��Ͽ� �̵��ϼ���", movingTutorialImg);
                //���� �̵� ����� �˷��ִ� UI ���
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

                //��Ʈ�ѷ��� ���� �̵��ϸ� �̵� UI ��Ȱ��ȭ
                // �÷��̾� �̵� ����� ������ ���
                yield return new WaitUntil(() => isPlayerMoved);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "�׸� ��ư�� ������ ���� ������.", gripTutorialImg);
                //����� �ձ��� �̵� �� ���ʷ� 1�� �׸� ��ư�� ���� UI ������ Ȱ��ȭ 
                yield return new WaitUntil(() => isTutorialArea);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

                //��Ʈ�ѷ��� �̿��Ͽ� �׸� ��ư�� ������ �׸� UI ��Ȱ��ȭ (����� ���� ���Ƶ� ������ �ʴ´�.)
                yield return new WaitUntil(() => isGripButtonPress);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);


                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                   "���� ��Ʈ�ѷ��� ��ƽ�� �Ĺ����� �Է��Ͽ� ��������", sitdownTutorialImg);
                //�ɱ� ��ư������ UI ������ Ȱ��ȭ
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
                //��Ʈ�ѷ� Ű�� �̿��Ͽ� ĳ���Ͱ� ������ �ɱ� UI ��Ȱ��ȭ 
                yield return new WaitUntil(() => isSitdown);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "�׸� ��ư�� ������ ������� ��������.", gripTutorialImg);
                //1�ʵ� ������ ��� ���� UI��� 
                yield return new WaitForSeconds(1f);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
                //��Ʈ�ѷ��� �̿��Ͽ� ������� ������ 1�ʵ� ������ ��� ���� UI ��Ȱ��ȭ
                yield return new WaitUntil(() => isTouchDrink && isGripButtonPress);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);
                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "B ��ư�� ������ ĵ �Ѳ��� ������", objectActiveTutorialImg);
                //������� ������ 1�ʵ� �������� ����ϴ� Ű�� �˷��ִ� UI Ȱ��ȭ 
                yield return new WaitForSeconds(1f);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

                //��Ʈ�ѷ��� �̿��Ͽ� ĵ �Ѳ��� ���� UI ��Ȱ��ȭ 
                yield return new WaitUntil(() => isCanOpen);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

                //������� ������ ���� ��(���� �α�)�� ��ġ��Ű�� ������� ���ô� ȿ���� ���.
                //ȿ������ ����Ǹ� ���� ��ܿ� �ִ� ����Ʈ UI ��Ȱ��ȭ ->ĳ���� ���� ��ȭ X
                yield return new WaitUntil(() => isCokeDrink);
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);

                GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
                    "[��Ʈ���� ������������ ��������]");
                GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.miniQuestText,
                    "[��Ʈ���� ������������ ��������]");
                //������� ���ð� 1�ʵ� �������� ������Ʈ�� ��ȣ�ۿ��� �ϴ� UI Ȱ��ȭ
                yield return new WaitForSeconds(1f);
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
                //ȭ�� �߾� �ϴܿ� [��Ʈ���� ������������ ��������] �ؽ�Ʈ 2�ʰ� ���
                yield return new WaitForSeconds(2f);
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
                //���� ��Ʈ�� �ؽ�Ʈ �ڽ��� ũ�⸦ ���� ũ���� 75%�� ����� �� ���� ������� ���� 
                GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);
                yield return new WaitUntil(() => throwCokecan);
                yield return new WaitForSeconds(1f);
                //������ ������Ʈ�� ��ȣ�ۿ��� �Ϸ�Ǹ� 1�ʵ� �ؽ�Ʈ�� ���� ��� UI ��Ȱ��ȭ
                GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(false);

                //���� �������� ��ȣ�ۿ��� �Ұ����ϴ�.
                GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
                    "[�ڵ����� ��������]");
                GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.miniQuestText,
                    "[�ڵ����� ��������]");

                yield return new WaitForSeconds(3f);
                //������ ����� ��ġ�� 3�� �� ��ȭ�� �Ҹ� ���, �̶� ��� ��Ʈ�ѷ��� ��ȭ���� �︮�� �ֱ⿡ ���� ��ƽ ����
                //�ڵ��� �������� ����ϱ� ������ �ݺ��ȴ�. �ڵ����� ���� ������ �Ÿ� ��� ����, �ڵ����� ������ �����ϴ�.
                //ȭ�� �߾� �ϴܿ� [�ڵ����� ��������] �ؽ�Ʈ 2�ʰ� ���
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
                //��ƽ�� �޼���� �� ��
                leftXRController.SendHapticImpulse(0.5f, 2f);
                rightXRController.SendHapticImpulse(0.5f, 2f);
                yield return new WaitForSeconds(2f);

                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                    "Y ��ư�� ������ �κ��丮�� ������", objectActiveTutorialImg);
                //���� ��Ʈ�� �ؽ�Ʈ ũ�Ⱑ 75%�� ��ҵ� �� ���� ������� �̵��Ͽ� �����ȴ�.
                GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
                GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);
                //�̶� �κ��丮 ����� �˷��ִ� UI�� Ȱ��ȭ �ȴ�.
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
                //�κ��丮�� ���� �κ��丮 �˸� UI ��Ȱ��ȭ
                yield return new WaitUntil(() => isInventoryOpen);
                GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);
                //������� ���� ���� ���� ������� �ڵ��� ��ȣ�ۿ�
                //������ ��� ��ư�� ������ ��ȭ�� �Ҹ��� ��ƽ ���� ����, ���� ��ܿ� �ִ� ���û��� UI��Ȱ��ȭ
                yield return new WaitUntil(() => isGetThePhone);
                GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(false);
                //������ ��� ��ư�� ������ ��簡 ��µȴ�.
                secondTutorialDialog.gameObject.SetActive(true);
                yield return new WaitUntil(() => secondTutorialDialog.isDialogsEnd == true);

                //�̶� �ڵ��� ����ϴ� ���߿� �κ��丮�� �ڵ����� ���� �� ����.
                //��� ��簡 ����Ǹ� ȭ�鿡 ������ ȭ������ ���̵� �ƿ��ȴ�.
                StartCoroutine(GameManager.Instance.uiManager.fadeInOutObj.FadeOut());
                //�ڵ����� ��� ���� ���°� �����Ѵ�.
                //2���������� �Ѿ�� �ε� ȭ���� ��Ÿ����.
                GameManager.Instance.isStage1Clear = true;

                break;
            case STAGE.STAGE2:

                break;
            case STAGE.STAGE3:

                break;
            case STAGE.STAGE4:

                break;
            case STAGE.STAGE5:

                break;
            case STAGE.STAGE6:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RightHandSnapturn(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        if (Mathf.Abs(input.x) > 0.5f) // ������ �Է� ���� (X�� �Է��� 0.5 �̻��� ���)
        {
            isSnapTurned = true;
            Debug.Log("������ ���� ����");
        }
    }
    private void LeftHandMove(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            isPlayerMoved = true;
            Debug.Log("��Ʈ�ѷ� �̵� �Է� ����");
        }
    }
    private void GripButtonPressed(InputAction.CallbackContext callback)
    {
        isGripButtonPress = true;
        Debug.Log("�׸� ��ư ����");
    }

    private void GripButtonReleased(InputAction.CallbackContext callback)
    {
        isGripButtonPress = false;
        Debug.Log("�׸� ��ư ����");
    }
    private void OpenCan(InputAction.CallbackContext callback)
    {
        if (isTouchDrink && isGripButtonPress)
        {
            isCanOpen = true;
        }
    }
}
