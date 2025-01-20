using System.Collections;
using System.Collections.Generic;
using echo17.EndlessBook;
using EPOOutline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Book : MonoBehaviour
{

    private EndlessBook endlessBook;
    [SerializeField]
    private InputActionReference l_Ref;
    [SerializeField]
    private InputActionReference r_Ref;

    private XRGrabInteractable xRGrabInteractable;
    public int currentPage;
    public int unLockCount = 1;
    private bool isInteract = false;
    private bool isDissolveChanging = false;
    private void Awake()
    {
        endlessBook = GetComponent<EndlessBook>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        endlessBook.SetState(EndlessBook.StateEnum.ClosedFront);
    }
    private void OnEnable()
    {
        l_Ref.action.performed += L;
        r_Ref.action.performed += R;
        xRGrabInteractable.enabled = true;
        GetComponent<Outlinable>().OutlineLayer = 0;
    }
    private void OnDisable()
    {
        l_Ref.action.performed -= L;
        r_Ref.action.performed -= R;
        GetComponent<Outlinable>().enabled = false;
    }

    public void OnSelectEnter()
    {
        if (unLockCount == 1)
        {
            SetActivatePage(unLockCount);
            unLockCount++;
        }
        StartCoroutine(OnSelectEnterCoroutine());
    }
    public void OnSelectExit()
    {
        isInteract = false;
        StartCoroutine(OnSelectExitCoroutine());
    }

    private void L(InputAction.CallbackContext context)
    {
        if (isInteract == false && isDissolveChanging) return;
        endlessBook.TurnBackward(0.5f);
    }
    private void R(InputAction.CallbackContext context)
    {
        if (isInteract == false && isDissolveChanging) return;
        endlessBook.TurnForward(0.5f);
    }
    private IEnumerator OnSelectEnterCoroutine()
    {
        yield return new WaitWhile(() => isInteract);
        endlessBook.TurnToPage(currentPage, EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime, 1f, 0.5f);
        yield return new WaitWhile(() => endlessBook.IsTurningPages);
        isInteract = true;
    }
    private IEnumerator OnSelectExitCoroutine()
    {
        xRGrabInteractable.enabled = false;
        yield return new WaitWhile(() => isDissolveChanging);
        // yield return new WaitUntil(() => currentPage == endlessBook.CurrentPageNumber);
        endlessBook.TurnToPage(1, EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime, 1f);
        yield return new WaitWhile(() => endlessBook.IsTurningPages);
        endlessBook.SetState(EndlessBook.StateEnum.ClosedFront);
        yield return new WaitUntil(() => endlessBook.CurrentState == EndlessBook.StateEnum.ClosedFront);
        //TODO : 인벤토리 들어가는 처리
        gameObject.SetActive(false);
    }

    #region 페이지 활성화시킬 시 호출
    public void SetActivatePage(int page)
    {
        StartCoroutine(DissolveCoroutine(page));
    }

    private IEnumerator DissolveCoroutine(int page)
    {
        isDissolveChanging = true;
        yield return new WaitWhile(() => endlessBook.IsTurningPages);
        endlessBook.TurnToPage(page, EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime, 1f);
        yield return new WaitWhile(() => endlessBook.IsTurningPages);
        while (true)
        {
            float a = endlessBook.GetPageData(page).material.GetFloat("_Dissolve");
            yield return new WaitWhile(() => endlessBook.IsTurningPages);
            endlessBook.GetPageData(page).material.SetFloat("_Dissolve", Mathf.Lerp(a, a - 0.1f, 0.0f));
            if (a < 0.01)
            {
                isDissolveChanging = false;
                break;
            }
            Debug.Log("도는중");
        }
    }
    #endregion
}
