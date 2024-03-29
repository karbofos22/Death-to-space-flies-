using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HpBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [Inject] private PlayerBehaviour player;

    private void Start()
    {
        slider.maxValue = player.hpHudValue;
        slider.value = slider.maxValue;
    }
    private void Update()
    {
       UpdateHp(player.hpHudValue);
    }

    public void UpdateHp(float amount)
    {
        slider.value = amount;
    }
}
