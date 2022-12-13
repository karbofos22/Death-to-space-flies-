using TMPro;
using UnityEngine;

public class ScoresUpdate : MonoBehaviour
{
    private int scores = 0;

    void Awake()
    {
        GlobalEventManager.OnEnemyKilled.AddListener( scoreAmount =>
        {
            scores += scoreAmount;
            GetComponent<TextMeshProUGUI>().text = $"{scores}";
        });
    }
}
