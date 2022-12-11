using UnityEngine;

public class BeamRayWeapon : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject BeamProjectile;
    [SerializeField] private BeamLoadingBar beamLoadingBar;

    private float currentBeamChargeValue;
    private readonly float maxBeamChargeValue = 100;
    private readonly float beamCost = 0.2f;

    [HideInInspector]
    public bool isLoaded;
    #endregion

    private void Start()
    {
        currentBeamChargeValue = 0;
    }
    void Update()
    {
        Shoot();
        BeamStatus();
    }
    void Shoot()
    {
        if (Input.GetButton("Fire2") && isLoaded)
        {
            BeamProjectile.SetActive(true);
            BeamDischarge(beamCost);
        }
        else
        {
            BeamProjectile.SetActive(false);
        }
    }
    public void BeamCharge(float chargeAmount)
    {
        currentBeamChargeValue += chargeAmount;
        if (currentBeamChargeValue > maxBeamChargeValue)
        {
            currentBeamChargeValue = maxBeamChargeValue;
        }
        beamLoadingBar.SetLoadingStatus(currentBeamChargeValue);
    }
    public void BeamDischarge(float amount)
    {
        currentBeamChargeValue -= amount;
        beamLoadingBar.SetLoadingStatus(currentBeamChargeValue);
        if (currentBeamChargeValue <= 0)
        {
            isLoaded = false;
        }
    }
    void BeamStatus()
    {
        if (currentBeamChargeValue <= 0)
        {
            isLoaded = false;
        }
        else
        {
            isLoaded = true;
        }
    }
}
