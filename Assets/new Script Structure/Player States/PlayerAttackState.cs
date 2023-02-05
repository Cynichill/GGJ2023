using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerStates
{

    private bool m_FacingRight;
    private bool m_FacingUp;
    private float moveSpeed = 10;
    private Rigidbody2D rb;

    private Vector2 moveThisFrame = Vector2.zero;

    //Set active item to axe initially
    private Item item = new ItemAxe();
    private Item secondaryItem = new ItemFlamethrower();

    public override void Initiate(PlayerManager playerRef, GameObject obj)
    {
        base.Initiate(playerRef, obj);
        rb = obj.GetComponent<Rigidbody2D>();
    }

    public override void ActionPrimary()
    {
        base.ActionPrimary();
        Debug.Log("Performing item action");

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
        return moveThisFrame;
    }

    public override void HandleMoveInput(Vector2 movement)
    {

        //LOGAN YOUR SCUFFED MOVE CODE GOES BELOW HERE

        // If the input is moving the player right and the player is facing left...
        if (movement.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && m_FacingRight)
        {
            Flip();
        }

        moveThisFrame = movement * moveSpeed;
    }

    public override void StopMoveInput(Vector2 movement)
    {
        if (movement == new Vector2(0, 0))
        {
            moveThisFrame = Vector2.zero;
        }
        else if(movement.x != 0)
        {
            moveThisFrame.y = 0;
        }
        else if(movement.y != 0)
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
}
