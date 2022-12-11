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
    [SerializeField] private int bossScores = 1000;
    [SerializeField] private UiManager uiManager;
    private bool isBossTime;
    #endregion

    private void Start()
    {
        AddScoresListener();
        AddGameOverListener();
    }
    private void Update()
    {
        BossSpawnConditions();
        ShowCursor();
    }
    #region Listeners
    private void AddScoresListener()
    {
        GlobalEventManager.OnEnemyKilled.AddListener(scoreAmount =>
        {
            scores += scoreAmount;
        });
    }
    private void AddGameOverListener()
    {
        GlobalEventManager.GameOver.AddListener(() =>
        {
            uiManager.GameOverScreenChange();
            isGameActive = false;
        });
    }
    #endregion

    #region Methods
    
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
