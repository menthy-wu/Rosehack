using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    bool is_crouch = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
    }
    void HandleAnimation()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            is_crouch = false;
            animator.Play("crouch_end");
            return;
        }
        if(is_crouch)
        {   
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("crouch_kick");
                return;
            }
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("crouch_start")||animator.GetCurrentAnimatorStateInfo(0).IsName("crouch_idle")||animator.GetCurrentAnimatorStateInfo(0).IsName("crouch_kick")||animator.GetCurrentAnimatorStateInfo(0).IsName("block_hold_alternatively_freeze")))
                animator.Play("crouch_start");
            else{
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.Play("crouch_idle");
                    return;
                }
            }
        }
        if(Input.GetKey(KeyCode.A) )
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("kick");
                return;
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                animator.Play("punch_heavier");
                return;
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("kick") || animator.GetCurrentAnimatorStateInfo(0).IsName("punch_heavier"))
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    animator.Play("walk_backward");
            }
            else{
                animator.Play("walk_backward");
            }
            return;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("kick");
                return;
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                animator.Play("punch_heavier");
                return;
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("kick") || animator.GetCurrentAnimatorStateInfo(0).IsName("punch_heavier"))
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    animator.Play("walk_forward");
            }
            else{
                animator.Play("walk_forward");
            }
            return;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("high_kick");
                return;
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("high_kick"))
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    animator.Play("walk_forward");
            }
            else{
                animator.Play("jump");
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
                animator.Play("punch_light");
                return;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play("kick_spin");
            return;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            is_crouch = true;
            return;

        }
        if(!is_crouch && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            animator.Play("idle");
    }
}
