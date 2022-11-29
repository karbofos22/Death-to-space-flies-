using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRayWeapon : MonoBehaviour
{
    public GameObject BeamProjectile;
    public bool isLoaded;

    private GameManager gameManager;
    public BeamLoadingBar beamLoadingBar;

    private float beamChargeProgress;
    readonly float beamCost = 0.2f;

    private void Start()
    {
        beamChargeProgress = 0;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            Shoot();
            BeamStatus();
        }
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
        beamChargeProgress += chargeAmount;
        if (beamChargeProgress > 100)
        {
            beamChargeProgress = 100;
        }
        beamLoadingBar.SetLoadingStatus(beamChargeProgress);
    }
    public void BeamDischarge(float amount)
    {
        beamChargeProgress -= amount;
        beamLoadingBar.SetLoadingStatus(beamChargeProgress);
        if (beamChargeProgress <= 0)
        {
            isLoaded = false;
        }
    }
    void BeamStatus()
    {
        if (beamChargeProgress <= 0)
        {
            isLoaded = false;
        }
        else
        {
            isLoaded = true;
        }
    }
}
