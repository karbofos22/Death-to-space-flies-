using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LaserWeapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> laserFirePoints;
    [SerializeField] private List<GameObject> powerUpLaserPoints;

    [Inject] private ObjectPooler objectPooler;
    [SerializeField] private Image laserPowerUpStatusImage;

    [HideInInspector] public bool isPowerUpActive;

    [HideInInspector] public float laserPowerUpLifeTime;

    private void Start()
    {
        laserPowerUpLifeTime = 0;
    }
    private void FixedUpdate()
    {
        StandartFiring();
        LaserPowerUpStatusUpdate();
    }
    private void StandartFiring()
    {
        foreach (var firePoint in laserFirePoints)
        {
            if (Input.GetButton("Fire1"))
            {
                var shot = objectPooler.pool.Get();
                shot.transform.position = firePoint.transform.position;

                shot.Init(objectPooler.Kill);
            }
        }
        if (isPowerUpActive)
        {
            foreach (var firePoint in powerUpLaserPoints)
            {
                if (Input.GetButton("Fire1"))
                {
                    var shot = objectPooler.pool.Get();
                    shot.transform.position = firePoint.transform.position;

                    shot.Init(objectPooler.Kill);
                }
            }
        }
    }
    private void LaserPowerUpStatusUpdate()
    {
        if (isPowerUpActive)
        {
            laserPowerUpStatusImage.gameObject.SetActive(true);

            laserPowerUpLifeTime -= Time.deltaTime;
            if (laserPowerUpLifeTime <= 0)
            {
                isPowerUpActive = false;
                laserPowerUpStatusImage.gameObject.SetActive(false);
            }
        }
        laserPowerUpStatusImage.GetComponentInChildren<TextMeshProUGUI>().text = $"{(int)laserPowerUpLifeTime}";
    }
}
