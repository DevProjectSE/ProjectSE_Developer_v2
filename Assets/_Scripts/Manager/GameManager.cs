using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{

    [Header("Player Object")]
    public GameObject Player;

    [Header("Manager")]
    public UIManager uiManager;
    public DataManager dataManager;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {

        SceneManager.sceneLoaded += (x, y) =>
        {
            if (SceneManager.GetActiveScene().name != "LoadingScene")
            {
                if (Player != null)
                {
                    Player = null;
                }
                if (Player == null)
                {
                    Player = FindAnyObjectByType<Player>().gameObject;
                }
            }
        };
    }
}
