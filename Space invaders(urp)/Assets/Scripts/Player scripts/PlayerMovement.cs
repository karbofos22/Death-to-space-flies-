using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private float steeringMod = 200f;

    const int border = 42;

    private GameManager gameManager;
    private PlayerBehaviour player;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerBehaviour>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            Movement();
        }
    }
    void Movement()
    {
        playerRb.velocity = new Vector3(Input.GetAxis("Horizontal") * steeringMod, 0);

        if (transform.position.x < -border)
        {
            transform.position = new Vector3(-border, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > border)
        {
            transform.position = new Vector3(border, transform.position.y, transform.position.z);
        }
        if (!player.isAlive)
        {
            steeringMod = 0;
        }
    }
}
