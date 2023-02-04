using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerStates
{

    private bool m_FacingRight;
    private float moveSpeed = 5;
    private Rigidbody2D rb;

    private Vector2 moveThisFrame = Vector2.zero;

    public override void Initiate(PlayerManager playerRef, GameObject obj)
    {
        base.Initiate(playerRef, obj);
        rb = obj.GetComponent<Rigidbody2D>();
    }

    public override void ActionPrimary()
    {
        base.ActionPrimary();
        Debug.Log("I am functional");
    }

    public override void HandleMoveInput(Vector2 movement)
    {
        
        Debug.Log("transformed movement called");
        //LOGAN YOUR SCUFFED MOVE CODE GOES BELOW HERE
        if (movement == new Vector2(0, 0))
        {
            //  ChangeAnimationState(PLAYER_IDLE);
        }
        else
        {
            // ChangeAnimationState(PLAYER_WALK);
        }

        // If the input is moving the player right and the player is facing left...
        if (movement.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && m_FacingRight)
        {
            Flip();
        }

        moveThisFrame = movement * moveSpeed * Time.deltaTime;
    }

    public override void Move()
    {

        rb.MovePosition(rb.position + moveThisFrame);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        playerObj.transform.Rotate(0f, 180f, 0f);
    }
}
