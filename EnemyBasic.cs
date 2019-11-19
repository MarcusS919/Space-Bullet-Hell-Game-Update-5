using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public float moveSpeed;                      // To set the enemy move speed
    public float health;                         // How much health the enemy have

   
    void Start()
    {
        // To set the movespeed of the enemy to 4 if it wasn't set
        if (moveSpeed == 0)
        {
            moveSpeed = 4;
        }
        
        // To set the health of the enemy to 3 if it wasn't set
        if (health == 0)
        {
            health = 3; 
        }
    }


    void Update()
    {

        // Move the enemy
        Move();
    }

    // Function for moving the enemy using the waypoints
    void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When collides with the player's projectiles, the enemy loses health and when it's health reaches 0 it dies
        if (collision.gameObject.tag == "Player_Projectile")
        {
            health -= 1;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
