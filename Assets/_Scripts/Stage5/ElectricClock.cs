using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricClock : MonoBehaviour
{
    public StageFifth stageFifth;
    public Button m_HourUp;
    public Button m_HourDown;
    public Button m_MinuteUp;
    public Button m_MinuteDown;

    public TextMeshProUGUI m_Hour;
    public TextMeshProUGUI m_Minute;

    public int startHour;
    public int startMinute;

    public XRSimpleInteractable xRSimpleInteractable;

    private void Awake()
    {
        if (xRSimpleInteractable == null) xRSimpleInteractable = GetComponentInParent<XRSimpleInteractable>();
        if (stageFifth == null) stageFifth = GetComponentInParent<StageFifth>();
        m_HourUp.onClick.AddListener(HourUp);
        m_HourDown.onClick.AddListener(HourDown);
        m_MinuteUp.onClick.AddListener(MinuteUp);
        m_MinuteDown.onClick.AddListener(MinuteDown);
    }

    private void Start()
    {
        if (startHour == 0) m_Hour.text = "0" + startHour.ToString();
        else m_Hour.text = startHour.ToString();
        if (startMinute % 10 != 0) m_Minute.text = "0" + startMinute.ToString();
        else if (startMinute == 0) m_Minute.text = "0" + startMinute.ToString();
        else m_Minute.text = startMinute.ToString();
    }
    private void HourUp()
    {
        startHour += 1;
        Debug.Log($"오름{startHour}");
        if (startHour < 10)
        {
            Debug.Log($"1 {startHour}");
            m_Hour.text = "0" + startHour.ToString();
        }
        else if (startHour == 24)
        {
            Debug.Log($"2 {startHour}");
            startHour = 0;
            m_Hour.text = "0" + startHour.ToString();
        }
        else
        {
            m_Hour.text = startHour.ToString();
        }
        ClearCheck();
    }
    private void HourDown()
    {
        startHour -= 1;
        if (startHour == -1)
        {
            startHour = 23;
        }
        if (startHour < 10)
        {
            m_Hour.text = "0" + startHour.ToString();
        }
        else
        {
            m_Hour.text = startHour.ToString();
        }
        ClearCheck();
    }
    private void MinuteUp()
    {
        startMinute += 10;
        if (startMinute == 60)
        {
            startMinute = 0;
            m_Minute.text = "0" + startMinute.ToString();
        }
        else
        {
            m_Minute.text = startMinute.ToString();
        }
        ClearCheck();
    }
    private void MinuteDown()
    {
        startMinute -= 10;
        if (startMinute == -10)
        {
            startMinute = 50;
        }
        if (startMinute == 0)
        {
            m_Minute.text = "0" + startMinute.ToString();
        }
        else
        {
            m_Minute.text = startMinute.ToString();
        }
        ClearCheck();
    }

    private void ClearCheck()
    {
        if (startHour == 17 && startMinute == 50)
        {
            stageFifth.ClockClear();
            xRSimpleInteractable.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
