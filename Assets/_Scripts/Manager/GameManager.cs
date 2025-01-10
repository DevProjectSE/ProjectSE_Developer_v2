using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("스테이지 클리어 여부")]
    [SerializeField, Tooltip("스테이지1 클리어 여부")] private bool isStage1Clear;
    [SerializeField, Tooltip("스테이지2 클리어 여부")] private bool isStage2Clear;
    [SerializeField, Tooltip("스테이지3 클리어 여부")] private bool isStage3Clear;
    [SerializeField, Tooltip("스테이지4 클리어 여부")] private bool isStage4Clear;
    [SerializeField, Tooltip("스테이지5 클리어 여부")] private bool isStage5Clear;

    [Header("엔딩 분기점")]
    [SerializeField, Tooltip("해피엔딩")] private bool isHappyEnding;
    [SerializeField, Tooltip("배드엔딩")] private bool isBadEnding;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
