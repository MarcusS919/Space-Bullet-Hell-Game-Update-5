using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour {


    // Handles movements and health
    public float moveSpeed;                      // To set the enemy move speed
    public float health;                         // How much health the enemy have
    public bool MoveLeft;                        // Controls when the object moves left or right

    // Handles projectile spawning
    public Rigidbody2D projectile;          // What to spawn
    public Transform projectileSpawnPoint;  // Where to spawn 
    public float projectileForce;           // How fast the projectile goes
    public float fireWaitTime;              // How fast the player can shoot
    public bool AllowFire;                  // Check if the player can shoot or not

    void Start()
    {
        InvokeRepeating("FireProjectile", 1.0f, fireWaitTime);


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

        AllowFire = true;

        MoveLeft = true;
    }


    void Update()
    {

        // Move the enemy
        Move();
    }

    // Function for moving the enemy using the waypoints
    void Move()
    {
        if (MoveLeft == true)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (MoveLeft == false)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When collides with the player's projectiles, the enemy loses health and when it's health reaches 0 it dies
        if (collision.gameObject.tag == "Player_Projectile")
        {
            health -= 1;
            if (health == 0)
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }

        if (collision.gameObject.tag == "LeftWall")
        {
            MoveLeft = false;
        }

        if (collision.gameObject.tag == "RightWall")
        {
            MoveLeft = true;
        }
    }

    void FireProjectile()
    {
        Debug.Log("Pew Pew");

        // Check if 'projectileSpawnPoint' and 'projectile' exist
        if (projectileSpawnPoint && projectile)
        {

            // Create the 'Projectile' and add to Scene
            Rigidbody2D temp = Instantiate(projectile, projectileSpawnPoint.position,
                projectileSpawnPoint.rotation);

            // Stop 'Character' from hitting 'Projectile'
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                temp.GetComponent<Collider2D>(), true);

            temp.AddForce(projectileSpawnPoint.up * projectileForce, ForceMode2D.Impulse);
        }
    }

}