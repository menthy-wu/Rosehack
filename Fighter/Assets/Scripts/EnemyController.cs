using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float jumpForce = 20f;
    public float moveSpeed = 5f;
    public Vector2 knockbackForce = new Vector2(5f, 15f);
    public bool facingRight = true;
    public GameObject enemy = null;
    public bool isJump = false;
    public bool isCrouch = false;
    public bool isAttacking = false;
    public bool isStunned = false;
    public int health = 200;
    public int knockbackThreshold = 50;
    public int currentKnockback = 0;
    // AI Actions { Jump, Right, Left, Attack}
    public bool[] AIActions = new bool[] { false, false, false, false };

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

        //AIDecision();

        //Debug.Log(AIActions[0] + " " + AIActions[1] + " " + AIActions[2] + " " + AIActions[3]);

        if (AIActions[0] && !isJump && !isStunned)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        }
        if (AIActions[1] && !isJump && !isStunned)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (AIActions[2] && !isJump && !isStunned)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (!isJump && !isStunned)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Hitting enemy with spacebar
        if (AIActions[3] && !isAttacking && !isStunned)
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
                    attackCooldown(1);
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
        int knockbackLevel = parameters[2];
        currentKnockback += (int)(knockbackLevel * (isCrouch ? 0.2f : 1));
        damage = (int)(damage * (isCrouch ? 0.2f : 1));
        Debug.Log("I took " + damage + " damage!");

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
            isStunned = true;
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
            if (collider.gameObject == enemy)
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
        StartCoroutine(stunCoroutine(duration));
    }
    IEnumerator stunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    void AIDecision()
    {
        // Reset AIActions
        for (int i = 0; i < AIActions.Length; i++)
        {
            AIActions[i] = false;
        }

        // If enemy is in range
        if (enemy != null)
        {
            // If enemy is above
            if (enemy.transform.position.y > transform.position.y + 1f)
            {
                // Jump
                AIActions[0] = true;
            }
            // If enemy is to the right
            if (enemy.transform.position.x > transform.position.x)
            {
                // Move right
                AIActions[1] = true;
            }
            // If enemy is to the left
            if (enemy.transform.position.x < transform.position.x)
            {
                // Move left
                AIActions[2] = true;
            }
            // If enemy is in range
            if (Mathf.Abs(enemy.transform.position.x - transform.position.x) < 1.4f)
            {
                // Attack
                AIActions[3] = true;
            }
        }
    }
}
