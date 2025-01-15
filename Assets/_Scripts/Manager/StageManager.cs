using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

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

    private bool isSnapTurned = false; // ������ ���� ���� �÷���
    private bool isPlayerMoved = false; //�÷��̾� �̵� ���� �÷���
    public bool inCokeArea = false; //ź��� ȹ�� ���� ��ġ �÷���
    public bool inTrashcanArea = false; //����� ź��� ������ ������ ��ġ �÷���
    public bool throwCokecan = false;   //������ ������ ���� �÷���

    private void OnEnable()
    {
        rightJoystick.action.performed += RightHandSnapturn;
        leftJoystick.action.performed += LeftHandMove;
    }
    private void OnDisable()
    {
        rightJoystick.action.performed -= RightHandSnapturn;
        leftJoystick.action.performed -= LeftHandMove;
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
                yield return new WaitUntil(()=> firstTutorialDialog.isDialogsEnd == true);

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

                //����� �ձ��� �̵� �� ���ʷ� 1�� �׸� ��ư�� ���� UI ������ Ȱ��ȭ 
                GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
                   "�׸� ��ư�� ������ ���� ������.", gripTutorialImg);
                //��Ʈ�ѷ��� �̿��Ͽ� �׸� ��ư�� ������ �׸� UI ��Ȱ��ȭ (����� ���� ���Ƶ� ������ �ʴ´�.)
                //�ɱ� ��ư������ UI ������ Ȱ��ȭ
                //��Ʈ�ѷ� Ű�� �̿��Ͽ� ĳ���Ͱ� ������ �ɱ� UI ��Ȱ��ȭ 
                //1�ʵ� ������ ��� ���� UI���
                //��Ʈ�ѷ��� �̿��Ͽ� ������� ������ 1�ʵ� ������ ��� ���� UI ��Ȱ��ȭ
                //������� ������ 1�ʵ� �������� ����ϴ� Ű�� �˷��ִ� UI Ȱ��ȭ 
                //��Ʈ�ѷ��� �̿��Ͽ� ĵ �Ѳ��� ���� UI ��Ȱ��ȭ 
                //������� ������ ���� ��(���� �α�)�� ��ġ��Ű�� ������� ���ô� ȿ���� ���.
                //ȿ������ ����Ǹ� ���� ��ܿ� �ִ� ����Ʈ UI ��Ȱ��ȭ ->ĳ���� ���� ��ȭ X
                //������� ���ð� 1�ʵ� �������� ������Ʈ�� ��ȣ�ۿ��� �ϴ� UI Ȱ��ȭ
                //ȭ�� �߾� �ϴܿ� [��Ʈ���� ������������ ��������] �ؽ�Ʈ 2�ʰ� ���
                //���� ��Ʈ�� �ؽ�Ʈ �ڽ��� ũ�⸦ ���� ũ���� 75%�� ����� �� ���� ������� ���� 
                //������ ������Ʈ�� ��ȣ�ۿ��� �Ϸ�Ǹ� 1�ʵ� �ؽ�Ʈ�� ���� ��� UI ��Ȱ��ȭ

                //���� �������� ��ȣ�ۿ��� �Ұ����ϴ�.
                //������ ����� ��ġ�� 3�� �� ��ȭ�� �Ҹ� ���, �̶� ��� ��Ʈ�ѷ��� ��ȭ���� �︮�� �ֱ⿡ ���� ��ƽ ����
                //�ڵ��� �������� ����ϱ� ������ �ݺ��ȴ�. �ڵ����� ���� ������ �Ÿ� ��� ����, �ڵ����� ������ �����ϴ�.
                //ȭ�� �߾� �ϴܿ� [�ڵ����� ��������] �ؽ�Ʈ 2�ʰ� ���
                //���� ��Ʈ�� �ؽ�Ʈ ũ�Ⱑ 75%�� ��ҵ� �� ���� ������� �̵��Ͽ� �����ȴ�.
                //�̶� �κ��丮 ����� �˷��ִ� UI�� Ȱ��ȭ �ȴ�.
                //�κ��丮�� ���� �κ��丮 �˸� UI ��Ȱ��ȭ
                //������� ���� ���� ���� ������� �ڵ��� ��ȣ�ۿ�
                //������ ��� ��ư�� ������ ��ȭ�� �Ҹ��� ��ƽ ���� ����, ���� ��ܿ� �ִ� ���û��� UI��Ȱ��ȭ

                //������ ��� ��ư�� ������ ��簡 ��µȴ�.
                //secondTutorialDialog.gameObject.SetActive(true);
                //yield return new WaitUntil(() => secondTutorialDialog.isDialogsEnd == true);

                //�̶� �ڵ��� ����ϴ� ���߿� �κ��丮�� �ڵ����� ���� �� ����.
                //��� ��簡 ����Ǹ� ȭ�鿡 ������ ȭ������ ���̵� �ƿ��ȴ�.
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
    
}
