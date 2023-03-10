    using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerStates
{

    private bool m_FacingRight;
    private bool m_FacingUp;
    private bool unchanged = false;
    private bool lrLast = false;
    private float moveSpeed = 10;
    private Rigidbody2D rb;
    private Animator anim;
    private string currentState;
    private Vector2 moveThisFrame = Vector2.zero;

    //Set active item to axe initially
    private Item item = new ItemAxe();
    private Item secondaryItem = new ItemFlamethrower();

    public override void Initiate(PlayerManager playerRef, GameObject obj)
    {
        base.Initiate(playerRef, obj);
        rb = obj.GetComponent<Rigidbody2D>();
        anim = obj.GetComponent<Animator>();
    }

    public override void ActionPrimary()
    {
        base.ActionPrimary();
        Debug.Log("Performing item action");

        unchanged = true;

        if (lrLast)
        {
            ChangeAnimationState("PlayerSideChop");
        }
        else if (m_FacingUp)
        {
            ChangeAnimationState("PlayerUpChop");
        }
        else
        {
            ChangeAnimationState("PlayerDownChop");
        }

        currentState = "";

        //Check in facing direction for enemy. If enemy found (vine), chop / burn
        item.Use(player);
    }

    public override void ActionSecondary()
    {
        base.ActionSecondary();

        secondaryItem.Use(player);
    }

    public override Vector2 GetMoveDirection()
    {
        if (moveThisFrame == Vector2.zero)
        {
            return player.currentMovement;
        } else
        {
            return moveThisFrame;
        }
    }

    public override void HandleMoveInput(Vector2 movement)
    {

        unchanged = false;

        if (movement.x != 0)
        {
            ChangeAnimationState("PlayerSideWalk");
            lrLast = true;
        }

        if (movement.x == 0)
        {
            if (movement.y > 0)
            {
                ChangeAnimationState("PlayerUpWalk");
                m_FacingUp = true;
                lrLast = false;
            }
            else if (movement.y < 0)
            {
                ChangeAnimationState("PlayerDownWalk");
                m_FacingUp = false;
                lrLast = false;
            }
        }

        //LOGAN YOUR SCUFFED MOVE CODE GOES BELOW HERE

        // If the input is moving the player right and the player is facing left...
        if (movement.x > 0 && m_FacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && !m_FacingRight)
        {
            Flip();
        }

        moveThisFrame = movement * moveSpeed;

        if (moveThisFrame.magnitude > 0.1)
        {
            player.currentMovement = moveThisFrame;
        }
    }

    public override void StopMoveInput(Vector2 movement)
    {
        if (movement == new Vector2(0, 0))
        {
            moveThisFrame = Vector2.zero;
            if (lrLast)
            {
                ChangeAnimationState("PlayerSideIdle");
            }
            else if (m_FacingUp)
            {
                ChangeAnimationState("PlayerUpIdle");
            }
            else
            {
                ChangeAnimationState("PlayerDownIdle");
            }

        }
        else if (movement.x != 0)
        {
            moveThisFrame.y = 0;
        }
        else if (movement.y != 0)
        {
            moveThisFrame.x = 0;
        }
    }

    public override void Move()
    {
        rb.MovePosition(rb.position + moveThisFrame * Time.deltaTime);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        playerObj.transform.Rotate(0f, 180f, 0f);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        //play the animation
        anim.Play(newState);
        //reassign the current state
        currentState = newState;
    }
}
