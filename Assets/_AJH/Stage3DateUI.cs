
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3DateUI : MonoBehaviour
{
    public StageFifth stageFifth;
    public Button Up1;
    public Button Down1;
    public Button Up2;
    public Button Down2;

    public TextMeshProUGUI firstNum;
    public TextMeshProUGUI secNum;

    public int startFirstNum;
    public int startSecNum;

    public XRSimpleInteractable xRSimpleInteractable;

    private void Awake()
    {
        if (xRSimpleInteractable == null) xRSimpleInteractable = GetComponentInParent<XRSimpleInteractable>();
        if (stageFifth == null) stageFifth = GetComponentInParent<StageFifth>();
        Up1.onClick.AddListener(FirstNumUp);
        Down1.onClick.AddListener(FirstNumDown);
        Up2.onClick.AddListener(SecNumUp);
        Down2.onClick.AddListener(SecNumDown);
    }

    private void Start()
    {
        if (startFirstNum == 0) firstNum.text = "0" + startFirstNum.ToString();
        else firstNum.text = startFirstNum.ToString();
        if (startSecNum % 10 != 0) secNum.text = "0" + startSecNum.ToString();
        else if (startSecNum == 0) secNum.text = "0" + startSecNum.ToString();
        else secNum.text = startSecNum.ToString();
    }
    private void FirstNumUp()
    {
        startFirstNum += 1;
        if (startFirstNum < 1)
        {
            firstNum.text = "0" + startFirstNum.ToString();
        }
        else if (startFirstNum == 24)
        {
            startFirstNum = 0;
            firstNum.text = "0" + startFirstNum.ToString();
        }
        else
        {
            firstNum.text = startFirstNum.ToString();
        }
        ClearCheck();
    }
    private void FirstNumDown()
    {
        startFirstNum -= 1;
        if (startFirstNum == -1)
        {
            startFirstNum = 23;
        }
        if (startFirstNum < 10)
        {
            firstNum.text = "0" + startFirstNum.ToString();
        }
        else
        {
            firstNum.text = startFirstNum.ToString();
        }
        ClearCheck();
    }
    private void SecNumUp()
    {
        startSecNum += 10;
        if (startSecNum == 60)
        {
            startSecNum = 0;
            secNum.text = "0" + startSecNum.ToString();
        }
        else
        {
            secNum.text = startSecNum.ToString();
        }
        ClearCheck();
    }
    private void SecNumDown()
    {
        startSecNum -= 10;
        if (startSecNum == -10)
        {
            startSecNum = 50;
        }
        if (startSecNum == 0)
        {
            secNum.text = "0" + startSecNum.ToString();
        }
        else
        {
            secNum.text = startSecNum.ToString();
        }
        ClearCheck();
    }

    private void ClearCheck()
    {
        if (startFirstNum == 17 && startSecNum == 50)
        {
            stageFifth.ClockClear();
            xRSimpleInteractable.enabled = false;
            gameObject.SetActive(false);
        }
    }
}

