using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyControllerAgent : Agent
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
    public float health = 200f;
    public int knockbackThreshold = 50;
    public int currentKnockback = 0;
    public float defense = 1;
    Animator animator;
    public Dictionary<string, bool> AIActions = new Dictionary<string, bool>()
    {
        {"jump", false},
        {"left", false},
        {"right", false},
        {"crouch", false},
        {"attack", false},
        {"special", false}
    };


    EnvironmentParameters defaultParameters;

    public override void Initialize()
    {
        defaultParameters = Academy.Instance.EnvironmentParameters;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position);
        sensor.AddObservation(gameObject.GetComponent<Rigidbody2D>().velocity);
        sensor.AddObservation(enemy.transform.position);
        sensor.AddObservation(enemy.GetComponent<Rigidbody2D>().velocity);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;
        AIActions["jump"] = discreteActions[0] == 1;
        AIActions["left"] = discreteActions[1] == 1;
        AIActions["right"] = discreteActions[2] == 1;
        AIActions["crouch"] = discreteActions[3] == 1;
        AIActions["attack"] = discreteActions[4] == 1;
        AIActions["special"] = discreteActions[5] == 1;
        // // log all actions in one line
        // string log = "";
        // foreach (var action in AIActions)
        // {
        //     log += action.Key + ": " + action.Value + ", ";
        // }
        // // Debug.Log(log);
        SetReward(-0.001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        discreteActionsOut[1] = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        discreteActionsOut[2] = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        discreteActionsOut[3] = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        discreteActionsOut[4] = Input.GetKey(KeyCode.Slash) ? 1 : 0;
        discreteActionsOut[5] = Input.GetKey(KeyCode.RightShift) ? 1 : 0;

        // var continuousActionsOut = actionsOut.ContinuousActions;
        // continuousActionsOut[0] = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        // continuousActionsOut[1] = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        // continuousActionsOut[2] = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        // continuousActionsOut[3] = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        // continuousActionsOut[4] = Input.GetKey(KeyCode.Slash) ? 1 : 0;
        // continuousActionsOut[5] = Input.GetKey(KeyCode.RightShift) ? 1 : 0;
    }
    public override void OnEpisodeBegin()
    {
        health = 200;
        currentKnockback = 0;
        isJump = false;
        isCrouch = false;
        isAttacking = false;
        isStunned = false;
        defense = 1;
        animator.Play("idle");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            animator.Play("die_loop");
            return;
        }
        // face enemy
        if (enemy != null)
        {
            if (enemy.transform.position.x > transform.position.x)
            {
                facingRight = true;
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            else
            {
                facingRight = false;
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
        }

        // AIDecision();

        bool jumpKey = AIActions["jump"];
        bool leftKey = AIActions["left"];
        bool rightKey = AIActions["right"];
        bool crouchKey = AIActions["crouch"];
        bool attackKey = AIActions["attack"];
        bool specialKey = AIActions["special"];

        if (isAttacking && !isStunned && !isJump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (crouchKey && !isJump && !isStunned) // Crouch
        {
            crouch();
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
            if (jumpKey && !isJump && !isStunned && !isAttacking) // Jump
            {
                // Set velocity to jumpForce
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
                animator.Play("jump");
            }
            if (rightKey && !isJump && !isStunned && !isAttacking) // Move right
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                animator.Play("walk_forward");
            }
            else if (leftKey && !isJump && !isStunned && !isAttacking) // Move left
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                animator.Play("walk_backward");
            }
            else if (!isJump && !isStunned) // Stop moving
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.Play("idle");
                }
            }
        }

        // Attacks
        if (attackKey && isCrouch && !isAttacking && !isJump && !isStunned) // Fast crouch kick
        {
            attack(transform.right, 1f, 0.5f, 4, 7);
            // Debug.Log("Fast crouch kick");
            animator.Play("crouch_kick");
        }
        else if (attackKey && isJump && !isAttacking && !isStunned) // High kick
        {
            attack(transform.right, 1.3f, 0.6f, 7, 15);
            // Debug.Log("High kick");
            animator.Play("high_kick");
        }
        else if (attackKey && (leftKey || rightKey) && !isJump && !isAttacking && !isStunned) // Side kick
        {
            delayedAttack(transform.right, 1.5f, 0.8f, 6, 12, 0.5f);
            // Debug.Log("Side kick");
            animator.Play("kick");
        }
        else if (attackKey && !isAttacking && !isStunned) // Jab
        {
            attack(transform.right, 1f, 0.3f, 3, 2);
            // Debug.Log("Jab");
            animator.Play("punch_light");
        }
        else if (specialKey && isCrouch && !isJump && !isAttacking && !isStunned) // Leg sweep
        {
            attack(transform.right, 2f, 0.8f, 10, 30);
            // Debug.Log("Leg sweep");
            animator.Play("crouch_end_kick_included");
        }
        else if (specialKey && (leftKey || rightKey) && isJump && !isAttacking && !isStunned) // Flying side kick
        {
            attack(transform.right, 2f, 0.8f, 10, 15);
            // Debug.Log("Flying side kick");
            animator.Play("kick");
        }
        else if (specialKey && (leftKey || rightKey) && !isJump && !isAttacking && !isStunned) // Strong punch
        {
            attack(transform.right, 2f, 0.8f, 8, 15);
            // Debug.Log("Strong punch");
            animator.Play("punch_heavier");
        }
        else if (specialKey && !isJump && !isAttacking && !isStunned) // Roundhouse
        {
            delayedAttack(transform.right, 2f, 1f, 20, 30, 0.6f);
            delayedAttack(transform.right, 2f, 1f, 30, 30, 1.61f);
            // Debug.Log("Roundhouse");
            animator.Play("kick_spin");
        }
        else if (isCrouch && (leftKey || rightKey) && !isJump && !isAttacking && !isStunned) // Dodge
        {
            if (rightKey) // Move right
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * 5, GetComponent<Rigidbody2D>().velocity.y);
                stun(0.1f);
                attack(Vector3.zero, 0f, 0.2f, 0, 0);
                animator.Play("crouch_kick");
            }
            else if (leftKey) // Move left
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed * 5, GetComponent<Rigidbody2D>().velocity.y);
                stun(0.1f);
                attack(Vector3.zero, 0f, 0.2f, 0, 0);
                animator.Play("crouch_kick");
            }
            // Debug.Log("Dodge");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = false;
            // Debug.Log("Ground!");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = true;
            // Debug.Log("Air!");
        }
    }
    void TakeDamage(int[] parameters)
    {
        int damage = parameters[0];
        bool facingRight = parameters[1] == 1;
        int knockbackLevel = parameters[2];
        currentKnockback += (int)(knockbackLevel * (isCrouch ? 0.2f : 1));
        damage = (int)(damage * (isCrouch ? 0.2f : 1) * defense);
        health -= damage;
        // Debug.Log("I took " + damage + " damage!");
        // Debug.Log("Knockback level " + currentKnockback);
        SetReward(damage * -0.01f);

        if (health <= 0)
        {
            health = 0;
            stun(100000);
            EndEpisode();
            animator.Play("die_loop");
            return;
        }

        if (isCrouch)
        {
            animator.Play("block_hold_alternatively_freeze");
        }
        else
        {
            animator.Play("stagger_1");
        }
        if (currentKnockback > knockbackThreshold)
        {
            currentKnockback = 0;
            if (facingRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce.x, knockbackForce.y);
                animator.Play("die_start");
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
                animator.Play("die_start");
            }
            stun(1);
        }
    }
    void delayedAttack(Vector3 offset, float radius, float cooldown, int damage, int knockbackLevel, float delay)
    {
        isAttacking = true;
        StartCoroutine(delayedAttackCoroutine(offset, radius, cooldown, damage, knockbackLevel, delay));
    }
    IEnumerator delayedAttackCoroutine(Vector3 offset, float radius, float cooldown, int damage, int knockbackLevel, float delay)
    {
        yield return new WaitForSeconds(delay);
        attack(offset, radius, cooldown, damage, knockbackLevel);
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
                SetReward(damage * 1f);
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
    void crouch()
    {
        if (!isCrouch)
        { animator.Play("crouch_start"); }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.Play("crouch_idle");
                return;
            }
        }
    }
    void AIDecision()
    {
        // Move towards the player
        if (transform.position.x < enemy.transform.position.x)
        {
            AIActions["right"] = true;
            AIActions["left"] = false;
        }
        else
        {
            AIActions["right"] = false;
            AIActions["left"] = true;
        }
        // Jump if the player is above
        if (transform.position.y < enemy.transform.position.y)
        {
            AIActions["jump"] = true;
        }
        else
        {
            AIActions["jump"] = false;
        }
        // Crouch if the player is below
        if (transform.position.y > enemy.transform.position.y)
        {
            AIActions["crouch"] = true;
        }
        else
        {
            AIActions["crouch"] = false;
        }
        // Attack if the player is in range
        if (Mathf.Abs(transform.position.x - enemy.transform.position.x) < 1.5f)
        {
            AIActions["attack"] = true;
        }
        else
        {
            AIActions["attack"] = false;
        }
        // Special attack if the player is in range
        if (Mathf.Abs(transform.position.x - enemy.transform.position.x) < 2.0f)
        {
            AIActions["special"] = true;
        }
        else
        {
            AIActions["special"] = false;
        }

    }
}
