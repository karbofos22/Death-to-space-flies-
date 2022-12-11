using UnityEngine;
using UnityEngine.UI;

public class BeamLoadingBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.maxValue = 100;
    }

    public void SetLoadingStatus(float amount)
    {
        slider.value = amount; 
    }
}
