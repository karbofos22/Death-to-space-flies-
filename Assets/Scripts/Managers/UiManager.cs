using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Fields
    [Header("UI objects")]
    public Image startScreen;
    public Image gameOverScreen;
    public Image gamePlayScreen;
    public Image tutorialScreen;
    public TextMeshProUGUI bossIncomeText;

    private readonly WaitForSeconds shortWait = new(0.7f);
    private bool bossIncoming;
    #endregion

    private void Start()
    {
        bossIncoming = false;
        AddBossTimeListener();
        AddBossFightListener();
        StartCoroutine(BossIncomingText());
    }
    private void AddBossTimeListener()
    {
        GlobalEventManager.BossIncoming.AddListener(() =>
        {
            bossIncoming = true;
        });
    }
    private void AddBossFightListener()
    {
        GlobalEventManager.BossFight.AddListener(() =>
        {
            bossIncoming = false;
        });
    }
    public void ShowTutorialScreen()
    {
        tutorialScreen.gameObject.SetActive(true);
    }
    public void HideTutorialScreen()
    {
        tutorialScreen.gameObject.SetActive(false);
    }
    public void GameOverScreenChange()
    {
        gamePlayScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void GameStartScreenChange()
    {
        startScreen.gameObject.SetActive(false);
        gamePlayScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GameRestartScreenChange()
    {
        gameOverScreen.gameObject.SetActive(false);
    }
    private IEnumerator BossIncomingText()
    {
        yield return new WaitUntil(() => bossIncoming);

        while (bossIncoming)
        {
            bossIncomeText.enabled = true;
            yield return shortWait;
            bossIncomeText.enabled = false;
            yield return shortWait;
        }
    }
}
