using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isJump = false;
    public float jumpForce = 20f;
    public float moveSpeed = 5f;
    public Vector2 knockbackForce = new Vector2(5f, 15f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.W) && !isJump)
        // {
        //     // Set velocity to jumpForce
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        //     // Print message to console
        //     Debug.Log("Jump!");
        // }
        // if (Input.GetKey(KeyCode.D) && !isJump)
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        // }
        // else if (Input.GetKey(KeyCode.A) && !isJump)
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        // }
        // else if (!isJump)
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        // }

        // // Hitting enemy with spacebar
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     // Get all colliders in a 1 unit radius in front of the player
        //     Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transform.right, 1f);
        //     // Loop through all colliders
        //     foreach (Collider2D collider in hitColliders)
        //     {
        //         // If the collider is a entity
        //         if (collider.gameObject.tag == "Entity")
        //         {
        //             // Send message to enemy to take damage
        //             collider.gameObject.SendMessage("TakeDamage", 1);
        //         }
        //     }
        // }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = false;
            Debug.Log("Ground!");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = true;
            Debug.Log("Air!");
        }
    }
    void TakeDamage(int[] parameters)
    {
        int damage = parameters[0];
        bool facingRight = parameters[1] == 1;
        Debug.Log("Enemy took " + damage + " damage!");
        if (facingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce.x, knockbackForce.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
        }
    }
}
