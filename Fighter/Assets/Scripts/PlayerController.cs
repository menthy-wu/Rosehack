using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isJump = false;
    public float jumpForce = 20f;
    public float moveSpeed = 5f;
    public Vector2 knockbackForce = new Vector2(5f, 15f);
    public bool facingRight = true;
    public GameObject enemy = null;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode attackKey = KeyCode.Space;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // face enemy
        if (enemy != null)
        {
            if (enemy.transform.position.x > transform.position.x)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
        }

        if (Input.GetKey(jumpKey) && !isJump)
        {
            // Set velocity to jumpForce
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            // Print message to console
            Debug.Log("Jump!");
        }
        if (Input.GetKey(rightKey))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (Input.GetKey(leftKey))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Hitting enemy with spacebar
        if (Input.GetKey(KeyCode.Space) && !isAttacking)
        {
            // Get all colliders in a 1 unit radius in front of the player
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transform.right * (facingRight ? 1 : -1), 1f);
            // Loop through all colliders
            foreach (Collider2D collider in hitColliders)
            {
                // If the collider is the enemy
                if (collider.gameObject == enemy)
                {
                    // Send message to enemy to take damage and direction
                    int[] parameters = new int[] { 1, facingRight ? 1 : 0 };
                    collider.gameObject.SendMessage("TakeDamage", parameters);
                    isAttacking = true;
                    attackCooldown();
                }
            }
        }



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
            GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce.x, knockbackForce.y);
        }
    }
    void attackCooldown()
    {
        StartCoroutine(attackCooldownCoroutine());
    }
    IEnumerator attackCooldownCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
