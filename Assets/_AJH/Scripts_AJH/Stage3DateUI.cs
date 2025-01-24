
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3DateUI : MonoBehaviour
{
    public GameObject dateUI;
    public GameObject homework;
    public GameObject button;


    public Button Up1;
    public Button Down1;
    public Button Up2;
    public Button Down2;

    public TextMeshProUGUI firstNum;
    public TextMeshProUGUI secNum;

    public int startFirstNum;
    public int startSecNum;

    public int collectFirstNumber;
    public int collectSecondNumber;

    public XRSimpleInteractable xRSimpleInteractable;

    public List<XRGripButton> keyPadButtons;

    public void KeyPadButtonsActivate()
    {
        foreach (XRGripButton xRGripButton in keyPadButtons)
        {
            xRGripButton.enabled = true;
        }
    }

    private void Awake()
    {
        if (xRSimpleInteractable == null) xRSimpleInteractable = GetComponentInParent<XRSimpleInteractable>();
        Up1.onClick.AddListener(FirstNumUp);
        Down1.onClick.AddListener(FirstNumDown);
        Up2.onClick.AddListener(SecNumUp);
        Down2.onClick.AddListener(SecNumDown);
    }


    private void Start()
    {
        firstNum.text = startFirstNum.ToString();
        secNum.text = startSecNum.ToString();
        homework.SetActive(false);

    }

    public void FirstNumUp()
    {
        startFirstNum += 1;
     
            int number = startFirstNum % 4; 
            firstNum.text = number.ToString();
        

        ClearCheck();
    }
    public void FirstNumDown()
    {
        startFirstNum -= 1;
        if (startFirstNum == -1)
        {
            startFirstNum = 3;
        }
        else
        {
            int number = startFirstNum % 4; 
            firstNum.text = number.ToString();
        }
       
        
   
        ClearCheck();
    }
    public void SecNumUp()
    {
        startSecNum += 1;
        int number = startSecNum % 10;
        secNum.text = number.ToString();
    
        ClearCheck();
    }
    public void SecNumDown()
    {
        startSecNum -= 1;
        if (startSecNum == -1)
        {
            startSecNum = 9;
        }

        int number = startSecNum % 10;
        secNum.text = number.ToString();

        ClearCheck();
    }

    private void ClearCheck()
    {
        if (startFirstNum == collectFirstNumber && startSecNum == collectSecondNumber)
        {
            xRSimpleInteractable.enabled = false;
            dateUI.SetActive(false);
            homework.SetActive(true);
            Destroy(button);
        }
    }
}

