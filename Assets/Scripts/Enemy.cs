using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyrb;
    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        enemyrb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyrb.AddForce(lookDirection * speed);

            if (transform.position.y < -10)
            {
                if (playerController != null)
                {
                    if (playerController.hasPowerUp && playerController.isGameOn)
                    {
                        playerController.AddScore(2);
                    }
                    else if(playerController.isGameOn)
                    {
                        playerController.AddScore(1);
                    }

                    if(playerController.isGameOn)
                    {
                      playerController.AddKill(1);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
