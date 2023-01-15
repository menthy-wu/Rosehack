using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 20f;
    public float moveSpeed = 5f;
    public Vector2 knockbackForce = new Vector2(5f, 15f);
    public bool facingRight = true;
    public GameObject enemy = null;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode crouchKey = KeyCode.S;
    public KeyCode attackKey = KeyCode.Space;
    public KeyCode specialKey = KeyCode.LeftShift;
    public bool isJump = false;
    public bool isCrouch = false;
    public bool isAttacking = false;
    public bool isStunned = false;
    public int health = 200;
    public int knockbackThreshold = 50;
    public int currentKnockback = 0;

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

        if (isAttacking && !isStunned)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(crouchKey) && !isAttacking && !isJump && !isStunned) // Crouch
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
            if (Input.GetKey(jumpKey) && !isJump && !isStunned && !isAttacking) // Jump
            {
                // Set velocity to jumpForce
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            }
            if (Input.GetKey(rightKey) && !isJump && !isStunned && !isAttacking) // Move right
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Input.GetKey(leftKey) && !isJump && !isStunned && !isAttacking) // Move left
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (!isJump && !isStunned) // Stop moving
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        // Attacks
        if (Input.GetKeyDown(attackKey) && isCrouch && !isAttacking && !isJump && !isStunned) // Fast crouch kick
        {
            attack(transform.right, 1f, 0.5f, 4, 7);
            Debug.Log("Fast crouch kick");
        }
        else if (Input.GetKeyDown(attackKey) && isJump && !isAttacking && !isStunned) // High kick
        {
            attack(transform.right, 1.3f, 0.6f, 7, 15);
            Debug.Log("High kick");
        }
        else if (Input.GetKeyDown(attackKey) && (Input.GetKey(leftKey) || Input.GetKey(rightKey)) && !isJump && !isAttacking && !isStunned) // Side kick
        {
            attack(transform.right, 1.5f, 0.8f, 6, 12);
            Debug.Log("Side kick");
        }
        else if (Input.GetKeyDown(attackKey) && !isAttacking && !isStunned) // Jab
        {
            attack(transform.right, 1f, 0.3f, 3, 2);
            Debug.Log("Jab");
        }
        else if (Input.GetKeyDown(specialKey) && isCrouch && !isJump && !isAttacking && !isStunned) // Leg sweep
        {
            attack(transform.right, 2f, 0.8f, 10, 30);
            Debug.Log("Leg sweep");
        }
        else if (Input.GetKeyDown(specialKey) && (Input.GetKey(leftKey) || Input.GetKey(rightKey)) && isJump && !isAttacking && !isStunned) // Flying side kick
        {
            attack(transform.right, 2f, 0.8f, 10, 15);
            Debug.Log("Flying side kick");
        }
        else if (Input.GetKeyDown(specialKey) && (Input.GetKey(leftKey) || Input.GetKey(rightKey)) && !isJump && !isAttacking && !isStunned) // Strong punch
        {
            attack(transform.right, 2f, 0.8f, 8, 15);
            Debug.Log("Strong punch");
        }
        else if (Input.GetKeyDown(specialKey) && !isJump && !isAttacking && !isStunned) // Roundhouse
        {
            attack(transform.right, 2f, 1f, 20, 51);
            Debug.Log("Roundhouse");
        }
        else if (isCrouch && (Input.GetKey(leftKey) || Input.GetKey(rightKey)) && !isJump && !isAttacking && !isStunned) // Dodge
        {
            if (Input.GetKey(rightKey)) // Move right
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * 5, GetComponent<Rigidbody2D>().velocity.y);
                stun(0.1f);
                attack(Vector3.zero, 0f, 0.2f, 0, 0);
            }
            else if (Input.GetKey(leftKey)) // Move left
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed * 5, GetComponent<Rigidbody2D>().velocity.y);
                stun(0.1f);
                attack(Vector3.zero, 0f, 0.2f, 0, 0);
            }
            Debug.Log("Dodge");
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
        int knockbackLevel = parameters[2];
        currentKnockback += (int)(knockbackLevel * (isCrouch ? 0.2f : 1));
        damage = (int)(damage * (isCrouch ? 0.2f : 1));
        health -= damage;
        Debug.Log("I took " + damage + " damage!");
        Debug.Log("Knockback level " + currentKnockback);

        if (health <= 0)
        {
            health = 0;
            stun(100000);
            return;
        }

        if (currentKnockback > knockbackThreshold)
        {
            currentKnockback = 0;
            if (facingRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce.x, knockbackForce.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
            }
            stun(1);
        }
    }
    void attack(Vector3 offset, float radius, float cooldown, int damage, int knockbackLevel)
    {
        isAttacking = true;
        // Get all colliders in a 1 unit radius in front of the player
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + offset * (facingRight ? 1 : -1), radius);
        // Loop through all colliders
        foreach (Collider2D collider in hitColliders)
        {
            // If the collider is the enemy
            if (collider.gameObject == enemy && !collider.isTrigger)
            {
                // Send message to enemy to take damage and direction
                int[] parameters = new int[] { damage, facingRight ? 1 : 0, knockbackLevel };
                collider.gameObject.SendMessage("TakeDamage", parameters);
            }
        }
        attackCooldown(cooldown);
    }
    void attackCooldown(float cooldown)
    {
        StartCoroutine(attackCooldownCoroutine(cooldown));
    }
    IEnumerator attackCooldownCoroutine(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }
    void stun(float duration)
    {
        isStunned = true;
        StartCoroutine(stunCoroutine(duration));
    }
    IEnumerator stunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}
