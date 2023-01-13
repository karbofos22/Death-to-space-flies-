using UnityEngine.Events;
using UnityEngine;

public class GlobalEventManager
{
    public static UnityEvent<int> OnEnemyKilled = new();
    public static UnityEvent GameIsActive = new();
    public static UnityEvent GameOver = new();
    public static UnityEvent BossIncoming = new();
    public static UnityEvent BossReadyToFight = new();
    public static UnityEvent BossDead = new();

    public static void SendEnemyKilled(int scoreAmount)
    {
        OnEnemyKilled.Invoke(scoreAmount);
    }
    public static void SendGameIsActive()
    {
        GameIsActive.Invoke();
    }
    public static void SendGameIsOver()
    {
        GameOver.Invoke();
    }
    public static void SendBossIncoming()
    {
        BossIncoming.Invoke();
    }
    public static void SendBossReadyToFight()
    {
        BossReadyToFight.Invoke();
    }
    public static void SendBossDead()
    {
        BossDead.Invoke();
    }
}

