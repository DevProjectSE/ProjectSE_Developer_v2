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

public class TutorialManager : MonoBehaviour
{
    [Header("Stage1_DialogSystem")]
    public DialogSystem firstTutorialDialog;
    public DialogSystem secondTutorialDialog;
    public DialogSystem thirdTutorialDialog;

    [Header("Stage1_Tutorial_Sprites")]
    public Sprite viewTutorialImg;
    public Sprite movingTutorialImg;
    public Sprite gripTutorialImg;
    public Sprite objectActiveTutorialImg;
    public Sprite putTutorialImg;
    public Sprite sitdownTutorialImg;
    public Sprite inventoryTutorialImg;

    public InputActionReference rightJoystick;
    public InputActionReference leftJoystick;

    public InputActionReference leftGripButton;
    public InputActionReference rightGripButton;

    public InputActionReference leftPrimaryButton;
    public InputActionReference leftSecondaryButton;
    public InputActionReference rightPrimaryButton;

    [Header("튜토리얼 동작 확인 플래그")]
    private bool isSnapTurned = false; // 스냅턴 실행 여부 플래그
    private bool isPlayerMoved = false; //플레이어 이동 여부 플래그

    public bool isTutorialArea = false; //튜토리얼 진행 가능 위치에 존재하는지 확인하는 플래그
    public bool isSitdown = false;  //앉았는지 확인하는 플래그
    private float previousY = 0f; // 이전 프레임의 Y값 저장

    public bool isTouchDrink = false;    //콜라가 손에 닿아있는지 확인하는 플래그
    private bool isGripButtonPress = false; //그립 버튼을 눌렀는지 확인하는 플래그
    private bool isCanOpen = false; //콜라 캔을 땄는지 확인하는 플래그
    public bool isCokeDrink = false;    //콜라 마셨는지 확인하는 플래그
    public bool throwCokecan = false;   //쓰레기 버리기 여부 플래그

    public bool isInventoryOpen = false;    //인벤토리 실행여부 확인 플래그

    private bool isCalling = false; //전화벨이 울리는 상태인지 확인하는 플래그
    private Coroutine hapticCoroutine; // 실행 중인 코루틴을 저장할 변수

    public bool isPhoneGrip = false;
    public bool isGetThePhone = false;  //전화를 받았는지 확인하는 플래그

    [Header("햅틱")]
    public XRBaseController leftXRController;
    public XRBaseController rightXRController;

    [Header("사운드")]
    public AudioSource audioSource;
    public AudioSource playerPhoneAudioSource;
    [Header("오디오 클립")]
    public AudioClip canOpenSound;
    public AudioClip drinkSound;
    public AudioClip canFall;

    [Header("핸드폰 사운드 클립")]
    public AudioClip callingPhone;
    public AudioClip callingEnd;

    private void OnEnable()
    {
        //출력 이후로 모두 옮기기
        rightJoystick.action.performed += RightHandSnapturn;
        leftJoystick.action.performed += LeftHandMove;
        rightJoystick.action.performed += RightHandSitdown;

        leftGripButton.action.performed += GripButtonPressed;
        leftGripButton.action.canceled += GripButtonReleased;
        rightGripButton.action.performed += GripButtonPressed;
        rightGripButton.action.canceled += GripButtonReleased;

        leftSecondaryButton.action.performed += InventoryOpen;
        leftPrimaryButton.action.performed += UsedItem;
        rightPrimaryButton.action.performed += UsedItem;

    }
    private void OnDisable()
    {
        rightJoystick.action.performed -= RightHandSnapturn;
        leftJoystick.action.performed -= LeftHandMove;
        rightJoystick.action.performed -= RightHandSitdown;

        leftGripButton.action.performed -= GripButtonPressed;
        leftGripButton.action.canceled -= GripButtonReleased;
        rightGripButton.action.performed -= GripButtonPressed;
        rightGripButton.action.canceled -= GripButtonReleased;

        leftSecondaryButton.action.performed -= InventoryOpen;
        leftPrimaryButton.action.performed -= UsedItem;
        rightPrimaryButton.action.performed -= UsedItem;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        //오브젝트를 쭉 훑어본 후 플레이어의 위치로 카메라가 이동하며 이후 플레이어가 움직일 수 있다.
        GameManager.Instance.Player.GetComponentInChildren<CustomPlayerController>().CtrlRelease();
        //대사 내용 출력 이후 플레이어 캐릭터 조작 활성화
        firstTutorialDialog.gameObject.SetActive(true);
        yield return new WaitUntil(() => firstTutorialDialog.isDialogsEnd == true);

        //퀘스트 내용 초기화
        GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
            "부엌으로 이동하여 냉장고에서 음료수를 꺼내 마시세요.");
        //퀘스트 알림 UI 활성화 2초간 활성화 이후 해당 UI가 축소되고 좌측 상단에 고정
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
        GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);

        //출력 전 UI 초기화
        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "우측 컨트롤러의 스틱을 이용하여 방향을 돌리세요.", viewTutorialImg);
        //시야 방향키 알림 UI 오른쪽에 출력
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

        //컨트롤러를 통해 시야 방향이 변경되면 시야 알림 UI 비활성화
        // 스냅턴 실행될 때까지 대기
        yield return new WaitUntil(() => isSnapTurned);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

        // UI 이동 방법 내용으로 초기화
        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "좌측 컨트롤러의 스틱을 이용하여 이동하세요", movingTutorialImg);
        //이후 이동 방법을 알려주는 UI 출력
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

        //컨트롤러를 통해 이동하면 이동 UI 비활성화
        // 플레이어 이동 실행될 때까지 대기
        yield return new WaitUntil(() => isPlayerMoved);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "그립 버튼을 누르고 문을 여세요.", gripTutorialImg);
        //냉장고 앞까지 이동 시 최초로 1번 그립 버튼에 대한 UI 우측에 활성화 
        yield return new WaitUntil(() => isTutorialArea);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);

        //컨트롤러를 이용하여 그립 버튼이 눌리면 그립 UI 비활성화 (냉장고 문을 놓아도 닫히지 않는다.)
        yield return new WaitUntil(() => isGripButtonPress);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
           "우측 컨트롤러의 스틱을 후방으로 입력하여 앉으세요", sitdownTutorialImg);
        //앉기 버튼에대한 UI 우측에 활성화
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
        //컨트롤러 키를 이용하여 캐릭터가 앉으면 앉기 UI 비활성화 
        yield return new WaitUntil(() => isSitdown);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);

        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "그립 버튼을 눌러서 음료수를 잡으세요.", gripTutorialImg);

        //1초뒤 아이템 잡기 설명 UI출력 
        yield return new WaitForSeconds(1f);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
        //컨트롤러를 이용하여 음료수를 잡으면 1초뒤 아이템 잡기 설명 UI 비활성화
        yield return new WaitUntil(() => isTouchDrink && isGripButtonPress);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);
        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "B 버튼을 눌러서 캔 뚜껑을 따세요", objectActiveTutorialImg);
        //음료수를 잡으면 1초뒤 아이템을 사용하는 키를 알려주는 UI 활성화 
        yield return new WaitForSeconds(1f);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
        audioSource.clip = canOpenSound; //오디오 클립 교체
        //컨트롤러를 이용하여 캔 뚜껑을 따면 UI 비활성화 
        yield return new WaitUntil(() => isCanOpen);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);
        yield return null;
        audioSource.clip = drinkSound; //오디오 클립 교체

        //음료수를 지정된 구역 내(헤드셋 부근)로 위치시키면 음료수를 마시는 효과음 재생.
        //효과음이 종료되면 좌측 상단에 있는 퀘스트 UI 비활성화 ->캐릭터 상태 변화 X
        yield return new WaitUntil(() => isCokeDrink);
        audioSource.PlayOneShot(drinkSound, 1.0f); //특정 클립 한번 만 재생
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);

        yield return null;
        audioSource.clip = canFall; //오디오 클립 교체

        GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
            "[페트병을 쓰레기통으로 버리세요]");
        GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.miniQuestText,
            "[페트병을 쓰레기통으로 버리세요]");
        //음료수를 마시고 1초뒤 아이템을 오브젝트와 상호작용을 하는 UI 활성화
        yield return new WaitForSeconds(1f);
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
        //화면 중앙 하단에 [페트병을 쓰레기통으로 버리세요] 텍스트 2초간 출력
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
        //이후 폰트와 텍스트 박스의 크기를 원래 크기의 75%로 축소한 후 우측 상단으로 고정 
        GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);
        yield return new WaitUntil(() => throwCokecan);
        audioSource.PlayOneShot(canFall, 1.0f); //특정 클립 한번 만 재생
        playerPhoneAudioSource.clip = callingPhone;
        yield return new WaitForSeconds(1f);
        //아이템 오브젝트의 상호작용이 완료되면 1초뒤 텍스트와 우측 상단 UI 비활성화
        GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(false);

        //버린 아이템은 상호작용이 불가능하다.
        GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.questText,
            "[핸드폰을 받으세요]");
        GameManager.Instance.uiManager.ChangeTutorialText(GameManager.Instance.uiManager.miniQuestText,
            "[핸드폰을 받으세요]");
        playerPhoneAudioSource.Play();//반복재생
        yield return new WaitForSeconds(3f);
        //아이템 사용을 마치고 3초 뒤 전화벨 소리 재생, 이때 모든 컨트롤러는 전화벨이 울리는 주기에 맞춰 햅틱 반응
        //핸드폰 아이템을 사용하기 전까지 반복된다. 핸드폰과 유저 사이의 거리 상관 없이, 핸드폰의 음량은 동일하다.
        //화면 중앙 하단에 [핸드폰을 받으세요] 텍스트 2초간 출력
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(true);
        isCalling = true;
        SetCallingState(isCalling);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.ChanageAllTutorialUI(GameManager.Instance.uiManager.controllerQuestText, GameManager.Instance.uiManager.controllerQuestImg,
            "Y 버튼을 눌러서 인벤토리를 여세요", inventoryTutorialImg);
        //이후 폰트와 텍스트 크기가 75%로 축소된 후 좌측 상단으로 이동하여 고정된다.
        GameManager.Instance.uiManager.mainQuestUiObj.SetActive(false);
        GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(true);
        //이때 인벤토리 출력을 알려주는 UI가 활성화 된다.
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(true);
        //인벤토리를 열면 인벤토리 알림 UI 비활성화
        yield return new WaitUntil(() => isInventoryOpen);
        GameManager.Instance.uiManager.controllerTutoObj.SetActive(false);
        //음료수를 마실 때와 같은 방법으로 핸드폰 상호작용
        //아이템 사용 버튼을 누르면 전화벨 소리와 햅틱 반응 종료, 좌측 상단에 있던 지시사항 UI비활성화
        yield return new WaitUntil(() => isPhoneGrip && isGripButtonPress);
        playerPhoneAudioSource.loop = false;
        playerPhoneAudioSource.Stop();
        yield return null;
        playerPhoneAudioSource.clip = callingEnd;
        yield return null;
        playerPhoneAudioSource.PlayOneShot(callingEnd, 1.0f);

        GameManager.Instance.uiManager.miniMainQuestUiObj.SetActive(false);
        secondTutorialDialog.gameObject.SetActive(true);
        yield return new WaitUntil(() => secondTutorialDialog.isDialogsEnd == true);
        //아이템 사용 버튼을 누르면 대사가 출력된다.
        yield return new WaitUntil(() => isPhoneGrip && isGetThePhone);
        isCalling = false;
        thirdTutorialDialog.gameObject.SetActive(true);
        yield return new WaitUntil(() => thirdTutorialDialog.isDialogsEnd == true);

        //이때 핸드폰 사용하는 도중에 인벤토리에 핸드폰을 넣을 수 없다. ->Select 상태에서 Exit상태로 변경이 불가능하다.
        //모든 대사가 종료되면 화면에 검은색 화면으로 페이드 아웃된다.
        StartCoroutine(GameManager.Instance.uiManager.fadeInOutObj.FadeOut());
        //핸드폰의 잡기 고정 상태가 해제한다.
        //2스테이지로 넘어가며 로딩 화면이 나타난다.
        //Scene 이동
        yield return new WaitForSeconds(3f);
        SceneLoadManager.Instance.StageLoad(StageNumber.Stage2);
    }

    private void RightHandSnapturn(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        if (Mathf.Abs(input.x) > 0.5f) // 스냅턴 입력 기준 (X축 입력이 0.5 이상일 경우)
        {
            isSnapTurned = true;
            Debug.Log("스냅턴 실행 감지");
        }
    }
    private void RightHandSitdown(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        float currentY = input.y;

        // 이전 Y값과 비교하여 감소하는 경우를 감지
        if (currentY < previousY)
        {
            isSitdown = true;
            Debug.Log("앉기 실행");
        }
        else
        {
            isSitdown = false;
        }

        // 현재 Y값을 다음 프레임의 이전 Y값으로 저장
        previousY = currentY;
    }

    private void LeftHandMove(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            isPlayerMoved = true;
            Debug.Log("컨트롤러 이동 입력 감지");
        }
    }
    private void GripButtonPressed(InputAction.CallbackContext callback)
    {
        isGripButtonPress = true;
        Debug.Log("그립 버튼 눌림");
    }

    private void GripButtonReleased(InputAction.CallbackContext callback)
    {
        isGripButtonPress = false;
        Debug.Log("그립 버튼 해제");
    }
    private void UsedItem(InputAction.CallbackContext callback)
    {
        if (isTouchDrink && isGripButtonPress)
        {
            Debug.Log("캔 뚜껑 열기");
            isCanOpen = true;
            audioSource.PlayOneShot(canOpenSound, 1.0f); //특정 클립 한번 만 재생
        }
        else if (isPhoneGrip && isGripButtonPress)
        {
            Debug.Log("핸드폰 사용");
            isGetThePhone = true;

        }
    }
    private void InventoryOpen(InputAction.CallbackContext callback)
    {
        Debug.Log("인벤토리 열기");
        isInventoryOpen = true;
    }
    /// <summary>
    /// isCalling 상태를 설정하고, 상태에 따라 햅틱 피드백을 시작하거나 중지합니다.
    /// </summary>
    /// <param name="state">true이면 햅틱 피드백 시작, false이면 중지</param>
    public void SetCallingState(bool state)
    {
        isCalling = state;

        if (isCalling && hapticCoroutine == null)
        {
            // isCalling이 true이고 코루틴이 실행 중이 아니면 코루틴 시작
            hapticCoroutine = StartCoroutine(HapticFeedbackLoop());
        }
        else if (!isCalling && hapticCoroutine != null)
        {
            // isCalling이 false이고 코루틴이 실행 중이면 코루틴 종료
            StopCoroutine(hapticCoroutine);
            hapticCoroutine = null;
        }
    }

    /// <summary>
    /// isCalling이 true인 동안 햅틱 피드백을 반복적으로 보냅니다.
    /// </summary>
    private IEnumerator HapticFeedbackLoop()
    {
        while (isCalling)
        {
            // 왼쪽 및 오른쪽 컨트롤러에 햅틱 피드백 전송 (강도: 0.5, 지속 시간: 0.2초)
            leftXRController.SendHapticImpulse(0.5f, 0.2f);
            rightXRController.SendHapticImpulse(0.5f, 0.2f);

            // 다음 햅틱 피드백까지 대기 (0.5초 간격)
            yield return new WaitForSeconds(0.5f);
        }
    }
}

