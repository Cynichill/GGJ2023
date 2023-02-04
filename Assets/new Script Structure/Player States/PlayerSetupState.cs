using System.Collections;
using UnityEngine;

public class PlayerSetupState : PlayerStates
{
    public override void ActionPrimary()
    {
        base.ActionPrimary();
        Debug.Log("I am functional");
    }

    public override void Move(Vector2 movement)
    {
        base.Move(movement);

        //LOGAN YOUR SCUFFED MOVE CODE GOES BELOW HERE

    }
}
