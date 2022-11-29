using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaserWeapons : MonoBehaviour
{
    public List<GameObject> laserFirePoints;
    public List<GameObject> powerUpLaserPoints;

    private ObjectPooler objectPooler;
    private GameManager gameManager;

    public Image laserPowerUpStatusImage;

    [HideInInspector]
    public bool isPowerUpActive;

    [HideInInspector]
    public float laserPowerUpLifeTime;

    void Start()
    {
        laserPowerUpLifeTime = 0;

        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        StandartFiring();
        LaserPowerUpStatusUpdate();
    }
    void StandartFiring()
    {
        foreach (var firePoint in laserFirePoints)
        {
            if (Input.GetButton("Fire1") && gameManager.isGameActive)
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
                if (Input.GetButton("Fire1") && gameManager.isGameActive)
                {
                    var shot = objectPooler.pool.Get();
                    shot.transform.position = firePoint.transform.position;

                    shot.Init(objectPooler.Kill);
                }
            }
        }
    }
    void LaserPowerUpStatusUpdate()
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
