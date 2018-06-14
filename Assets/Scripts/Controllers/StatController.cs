using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatHandler();

public class StatController : MonoBehaviour
{
    public static event StatHandler StartNewGame;

    [SerializeField] GameObject finishPanel;

    void Start ()
    {
        PlayerController.PlayerDeath += ShowStatistics;

        if (finishPanel)
            finishPanel.SetActive(false);
	}

    void ShowStatistics()
    {
        finishPanel.SetActive(true);
    }

    public void ResetGame()
    {
        StartNewGame();
    }
}
