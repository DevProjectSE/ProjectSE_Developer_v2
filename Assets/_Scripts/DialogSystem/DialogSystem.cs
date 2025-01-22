using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Speaker[] speakers; // Speaker�� Dialog �迭
    [SerializeField]
    private Dialog[] dialogs; // ���� �б��� ��� ��� �迭

    [SerializeField]
    private bool isAutoStart = true; // �ڵ� ���� ����
    [SerializeField]
    private float typingSpeed = 0.1f; // �ؽ�Ʈ Ÿ���� ȿ�� �ӵ�
    [SerializeField]
    private float dialogDelay = 2f; // ���� ���� �Ѿ�� �� ��� �ð�

    private int currentDialogIndex = -1; // ���� ��� ����
    private int currentDialogIndexNum = 0; // ���� ������ �ϴ� dialogIndex�� �迭 ����
    private bool isTypingEffect = false; // �ؽ�Ʈ Ÿ���� ȿ�� ��� ������ Ȯ���ϴ� ����
    public bool isDialogsEnd = false;  // ��ȭ�� ����Ǿ����� Ȯ���ϴ� ����

    private void Start()
    {
        InitializeSpeakers();

        if (isAutoStart)
        {
            StartCoroutine(AutoPlayDialog());
        }
    }

    /// <summary>
    /// Speaker UI ��Ҹ� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void InitializeSpeakers()
    {
        foreach (var speaker in speakers)
        {
            speaker.dialogImage.gameObject.SetActive(false);
            speaker.textName.gameObject.SetActive(false);
            speaker.textDialogue.gameObject.SetActive(false);
            speaker.objectArrow.SetActive(false);

            // �̸� �ʱ�ȭ
            speaker.textName.text = "";
            speaker.textDialogue.text = "";
        }

        currentDialogIndex = -1; // ��ȭ ������ ���� �ʱ�ȭ
        isDialogsEnd = false;    // ��ȭ ���� ���� �ʱ�ȭ
    }

    public IEnumerator AutoPlayDialog()
    {
        while (currentDialogIndex + 1 < dialogs.Length)
        {
            SetNextDialog(); // ���� ��� ����
            yield return new WaitForSeconds(dialogDelay + typingSpeed * dialogs[currentDialogIndexNum].dialogue.Length); // ���
        }

        // ��� ��簡 ���� �� ó��
        EndDialog();
    }

    private void SetNextDialog()
    {
        if (currentDialogIndex >= 0)
        {
            SetActiveObjects(speakers[currentDialogIndexNum], false); // ���� ��ȭ ��Ȱ��ȭ
        }

        // ���� ��縦 ����
        currentDialogIndex++;
        currentDialogIndexNum = dialogs[currentDialogIndex].dialogIndex;
        SetActiveObjects(speakers[currentDialogIndexNum], true);

        // �̸� ������Ʈ
        speakers[currentDialogIndexNum].textName.text = dialogs[currentDialogIndex].Name;

        // ��� Ÿ���� ȿ�� ����
        StartCoroutine(OnTypingText());
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.dialogImage.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);

        if (!visible)
        {
            speaker.objectArrow.SetActive(false); // Ŀ���� �׻� �ʱ�ȭ
        }
    }

    private IEnumerator OnTypingText()
    {
        string fullText = dialogs[currentDialogIndexNum].dialogue; // ��ü ���
        string displayedText = ""; // ���� ��� ���� �ؽ�Ʈ
        int charIndex = 0; // ��� ���� ���� �ε���
        isTypingEffect = true;

        while (charIndex < fullText.Length)
        {
            // RichText �±� ó��
            if (fullText[charIndex] == '<') // �±� ����
            {
                int endTagIndex = fullText.IndexOf('>', charIndex);
                if (endTagIndex != -1) // ��ȿ�� �±׶��
                {
                    displayedText += fullText.Substring(charIndex, endTagIndex - charIndex + 1);
                    charIndex = endTagIndex + 1;
                    continue; // �±� ó�� �� ���� ���ڷ� �Ѿ
                }
            }

            // �Ϲ� ���� �߰�
            displayedText += fullText[charIndex];
            speakers[currentDialogIndexNum].textDialogue.text = displayedText;
            charIndex++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
        speakers[currentDialogIndexNum].objectArrow.SetActive(true); // ��� �Ϸ� Ŀ�� Ȱ��ȭ
    }


    public void EndDialog()
    {
        foreach (var speaker in speakers)
        {
            SetActiveObjects(speaker, false);
        }
        isDialogsEnd = true;

        GameManager.Instance.Player.GetComponentInChildren<CustomPlayerController>().CtrlActivation();
        Debug.Log("��� ��簡 �Ϸ�Ǿ����ϴ�.");

        // �� ������Ʈ�� ��Ȱ��ȭ�Ͽ� ��ȭ ���Ḧ �˸�
        this.gameObject.SetActive(false);
    }

}

[System.Serializable]
public struct Speaker
{
    public Image dialogImage; // ��ȭâ �̹���
    public TextMeshProUGUI textName; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI textDialogue; // ��� �ؽ�Ʈ
    public GameObject objectArrow; // Ŀ��
}

[System.Serializable]
public struct Dialog
{
    public int dialogIndex; // ��縦 ����� Speaker �迭 ����
    public string Name; // �̸�
    [TextArea(5, 5)]
    public string dialogue; // ��� ����
}
