using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{

    [Header("Player Object")]
    public GameObject Player;

    [Header("Manager")]
    public UIManager uiManager;
    public DataManager dataManager;
    public List<Material> diary_Mats;
    protected override void Awake()
    {
        base.Awake();
        if (Player == null)
        {
            Player = FindAnyObjectByType<Player>().gameObject;
        }
    }
    private void Start()
    {

        SceneManager.sceneLoaded += (x, y) =>
        {
            if (SceneManager.GetActiveScene().name == "Stage1_Complete")
            {
                uiManager = FindAnyObjectByType<UIManager>();
            }
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {

                Player = FindAnyObjectByType<Player>().gameObject;
                int i = 0;
                foreach (bool a in DataManager.Instance.dataTable.isStageEnter)
                {
                    Player.GetComponentInChildren<Main_Title>().stageBTN[i].interactable = a;
                    i++;
                }
            }
        };

    }

    public void DiaryMat_Activate(int page)
    {
        diary_Mats[page - 1].SetFloat("_Dissolve", 0);
    }
}
