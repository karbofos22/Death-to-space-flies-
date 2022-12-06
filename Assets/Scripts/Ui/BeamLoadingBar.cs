using UnityEngine;
using UnityEngine.UI;

public class BeamLoadingBar : MonoBehaviour
{
    public Slider slider;

    private BeamRayWeapon beamRay;

    private void Start()
    {
        beamRay = GameObject.Find("Player").GetComponent<BeamRayWeapon>();

        slider.maxValue = 100;
    }

    public void SetLoadingStatus(float amount)
    {
        slider.value = amount; 
    }
    public void CanUseBeam()
    {
        if (slider.value == slider.maxValue)
        {
            beamRay.isLoaded = true;
        }
    }
}
