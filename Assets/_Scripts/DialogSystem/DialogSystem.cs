using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Speaker[] speakers; // Speaker의 Dialog 배열
    [SerializeField]
    private Dialog[] dialogs; // 현재 분기의 대사 목록 배열

    [SerializeField]
    private bool isAutoStart = true; // 자동 시작 여부
    [SerializeField]
    private float typingSpeed = 0.1f; // 텍스트 타이핑 효과 속도
    [SerializeField]
    private float dialogDelay = 2f; // 다음 대사로 넘어가기 전 대기 시간

    private int currentDialogIndex = -1; // 현재 대사 순번
    private int currentDialogIndexNum = 0; // 현재 설명을 하는 dialogIndex의 배열 순번
    private bool isTypingEffect = false; // 텍스트 타이핑 효과 재생 중인지 확인하는 변수
    public bool isDialogsEnd = false;  // 대화가 종료되었는지 확인하는 변수

    private void Start()
    {
        InitializeSpeakers();

        if (isAutoStart)
        {
            StartCoroutine(AutoPlayDialog());
        }
    }

    /// <summary>
    /// Speaker UI 요소를 초기화합니다.
    /// </summary>
    private void InitializeSpeakers()
    {
        foreach (var speaker in speakers)
        {
            speaker.dialogImage.gameObject.SetActive(false);
            speaker.textName.gameObject.SetActive(false);
            speaker.textDialogue.gameObject.SetActive(false);
            speaker.objectArrow.SetActive(false);

            // 이름 초기화
            speaker.textName.text = "";
            speaker.textDialogue.text = "";
        }

        currentDialogIndex = -1; // 대화 시작을 위한 초기화
        isDialogsEnd = false;    // 대화 종료 상태 초기화
    }

    public IEnumerator AutoPlayDialog()
    {
        while (currentDialogIndex + 1 < dialogs.Length)
        {
            SetNextDialog(); // 다음 대사 설정
            yield return new WaitForSeconds(dialogDelay + typingSpeed * dialogs[currentDialogIndexNum].dialogue.Length); // 대기
        }

        // 모든 대사가 끝난 후 처리
        EndDialog();
    }

    private void SetNextDialog()
    {
        if (currentDialogIndex >= 0)
        {
            SetActiveObjects(speakers[currentDialogIndexNum], false); // 이전 대화 비활성화
        }

        // 다음 대사를 진행
        currentDialogIndex++;
        currentDialogIndexNum = dialogs[currentDialogIndex].dialogIndex;
        SetActiveObjects(speakers[currentDialogIndexNum], true);

        // 이름 업데이트
        speakers[currentDialogIndexNum].textName.text = dialogs[currentDialogIndex].Name;

        // 대사 타이핑 효과 실행
        StartCoroutine(OnTypingText());
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.dialogImage.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);

        if (!visible)
        {
            speaker.objectArrow.SetActive(false); // 커서는 항상 초기화
        }
    }

    private IEnumerator OnTypingText()
    {
        string fullText = dialogs[currentDialogIndexNum].dialogue; // 전체 대사
        string displayedText = ""; // 현재 출력 중인 텍스트
        int charIndex = 0; // 출력 중인 문자 인덱스
        isTypingEffect = true;

        while (charIndex < fullText.Length)
        {
            // RichText 태그 처리
            if (fullText[charIndex] == '<') // 태그 시작
            {
                int endTagIndex = fullText.IndexOf('>', charIndex);
                if (endTagIndex != -1) // 유효한 태그라면
                {
                    displayedText += fullText.Substring(charIndex, endTagIndex - charIndex + 1);
                    charIndex = endTagIndex + 1;
                    continue; // 태그 처리 후 다음 문자로 넘어감
                }
            }

            // 일반 문자 추가
            displayedText += fullText[charIndex];
            speakers[currentDialogIndexNum].textDialogue.text = displayedText;
            charIndex++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
        speakers[currentDialogIndexNum].objectArrow.SetActive(true); // 대사 완료 커서 활성화
    }


    public void EndDialog()
    {
        foreach (var speaker in speakers)
        {
            SetActiveObjects(speaker, false);
        }
        isDialogsEnd = true;

        GameManager.Instance.Player.GetComponentInChildren<CustomPlayerController>().CtrlActivation();
        Debug.Log("모든 대사가 완료되었습니다.");

        // 이 오브젝트를 비활성화하여 대화 종료를 알림
        this.gameObject.SetActive(false);
    }

}

[System.Serializable]
public struct Speaker
{
    public Image dialogImage; // 대화창 이미지
    public TextMeshProUGUI textName; // 이름 텍스트
    public TextMeshProUGUI textDialogue; // 대사 텍스트
    public GameObject objectArrow; // 커서
}

[System.Serializable]
public struct Dialog
{
    public int dialogIndex; // 대사를 출력할 Speaker 배열 순번
    public string Name; // 이름
    [TextArea(5, 5)]
    public string dialogue; // 대사 내용
}
