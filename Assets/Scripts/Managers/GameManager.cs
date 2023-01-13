using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
    #region Fields
    [Header("Game statuses")]
    [SerializeField] private bool isGameActive;

    private int scores;
    [SerializeField] private int bossScores = 20000;
    [SerializeField] private UiManager uiManager;
    private bool isBossTime;
    #endregion

    private void Start()
    {
        GlobalEventManager.GameOver.AddListener(GameOver);
        GlobalEventManager.OnEnemyKilled.AddListener(AddScores);
    }
    private void Update()
    {
        BossSpawnConditions();
        ShowCursor();
    }

    #region Methods
    private void AddScores(int scoreAmount)
    {
        scores += scoreAmount;
    }
    private void GameOver()
    {
        uiManager.GameOverScreenChange();
        isGameActive = false;

    }
    private void BossSpawnConditions()
    {
        if (scores >= bossScores && !isBossTime)
        {
            isBossTime = true;
            GlobalEventManager.SendBossIncoming();
        }
    }

    public void GameStart()
    {
        uiManager.GameStartScreenChange();
        GlobalEventManager.SendGameIsActive();
        isGameActive = true;
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GlobalEventManager.GameOver.RemoveAllListeners();
        uiManager.GameRestartScreenChange();
    }
    private void ShowCursor()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    #endregion
}
