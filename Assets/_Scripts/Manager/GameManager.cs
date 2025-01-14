using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("스테이지 클리어 여부")]
    [Tooltip("스테이지1 클리어 여부")] public bool isStage1Clear;
    [Tooltip("스테이지2 클리어 여부")] public bool isStage2Clear;
    [Tooltip("스테이지3 클리어 여부")] public bool isStage3Clear;
    [Tooltip("스테이지4 클리어 여부")] public bool isStage4Clear;
    [Tooltip("스테이지5 클리어 여부")] public bool isStage5Clear;

    [Header("엔딩 분기점")]
    [SerializeField, Tooltip("해피엔딩")] public bool isHappyEnding;
    [SerializeField, Tooltip("배드엔딩")] public bool isBadEnding;

    [Header("Player Object")]
    public GameObject Player;

    [Header("Manager")]
    public UIManager uiManager;

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
