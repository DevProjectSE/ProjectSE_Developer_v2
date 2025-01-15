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
    private bool isInteract = false;

    private void Awake()
    {
        endlessBook = GetComponent<EndlessBook>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }
    private void OnEnable()
    {
        l_Ref.action.performed += L;
        r_Ref.action.performed += R;
        xRGrabInteractable.enabled = true;
        GetComponent<Outlinable>().enabled = false;
    }
    private void OnDisable()
    {
        l_Ref.action.performed -= L;
        r_Ref.action.performed -= R;
    }

    public void OnSelectEnter()
    {
        StartCoroutine(OnSelectEnterCoroutine());
    }
    public void OnSelectExit()
    {
        isInteract = false;
        StartCoroutine(OnSelectExitCoroutine());
    }

    private void L(InputAction.CallbackContext context)
    {
        if (isInteract == false) return;
        endlessBook.TurnBackward(0.5f);
    }
    private void R(InputAction.CallbackContext context)
    {
        if (isInteract == false) return;
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
        endlessBook.TurnToPage(1, EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime, 1f);
        yield return new WaitWhile(() => endlessBook.IsTurningPages);
        endlessBook.SetState(EndlessBook.StateEnum.ClosedFront);
    }
}
