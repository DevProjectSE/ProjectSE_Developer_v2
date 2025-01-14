using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class StageManager : MonoBehaviour
{
    public enum STAGE { STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 };
    public STAGE stage;

    [Header("Stage1_DialogSystem")]
    public DialogSystem FirstTutorialDialog;
    public DialogSystem SecondTutorialDialog;


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
                FirstTutorialDialog.gameObject.SetActive(true);

                //����Ʈ �˸� UI Ȱ��ȭ 2�ʰ� Ȱ��ȭ ���� �ش� UI�� ��ҵǰ� ���� ��ܿ� ����
                //�þ� ����Ű �˸� UI �����ʿ� ���
                //��Ʈ�ѷ��� ���� �þ� ������ ����Ǹ� �þ� �˸� UI ��Ȱ��ȭ
                //���� �̵� ����� �˷��ִ� UI ���
                //��Ʈ�ѷ��� ���� �̵��ϸ� �̵� UI ��Ȱ��ȭ

                //����� �ձ��� �̵� �� ���ʷ� 1�� �׸� ��ư�� ���� UI ������ Ȱ��ȭ 
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
                SecondTutorialDialog.gameObject.SetActive(true);

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
}
