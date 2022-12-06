using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game statuses")]
    public bool isGameActive;
    public bool isGameOver;

    [Header("UI objects")]
    public Image startScreen;
    public Image gameOverScreen;
    public Image gamePlayScreen;
    public Image tutorialScreen;
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI bossIncomeText;

    private int scores;

    public bool startBossInfo;
    public bool isBossTime;

    void Start()
    {
        isBossTime = false;

        StartCoroutine(IncomingInfo());
        StartCoroutine(CheckForBoss());
    }

    void Update()
    {
        GameOverSetup();
        ScoresUiUpdate();
        ShowCursor();
    }
    IEnumerator IncomingInfo()
    {
        yield return new WaitUntil(() => startBossInfo);

        while (startBossInfo)
        {
            bossIncomeText.enabled = true;
            yield return new WaitForSeconds(0.7f);
            bossIncomeText.enabled = false;
            yield return new WaitForSeconds(0.7f);
        }
    }
    public void ShowTutorialScreen()
    {
            tutorialScreen.gameObject.SetActive(true);
    }
    public void HideTutorialScreen()
    {
        tutorialScreen.gameObject.SetActive(false);
    }
    IEnumerator CheckForBoss()
    {
        yield return new WaitUntil(() => scores >= 20000);

        isBossTime = true;

    }
    void GameOverSetup()
    {
        if (isGameOver)
        {
            gamePlayScreen.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    void ScoresUiUpdate()
    {
        scoresText.text = $"{scores}";
    }
    public void ScoresIncrease(int scoreAmount)
    {
        scores += scoreAmount;
    }
    public void GameStart()
    {
        startScreen.gameObject.SetActive(false);
        gamePlayScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        isGameActive = true;
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.gameObject.SetActive(false);
    }
    void ShowCursor()
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
}
