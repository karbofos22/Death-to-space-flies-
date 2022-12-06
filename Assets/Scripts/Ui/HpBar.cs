using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;

    private PlayerBehaviour player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();

        slider.maxValue = player.hp;
        slider.value = slider.maxValue;
    }
    private void Update()
    {
        UpdateHp(player.hp);
    }
    void UpdateHp(float amount)
    {
        slider.value = amount;
    }
}
