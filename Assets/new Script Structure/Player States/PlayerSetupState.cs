using System.Collections;
using UnityEngine;

public class PlayerSetupState : PlayerStates
{
    public override void ActionPrimary()
    {
        base.ActionPrimary();
        Debug.Log("I am functional");
    }

    public override void HandleMoveInput(Vector2 movement)
    {
        base.HandleMoveInput(movement);

        //LOGAN YOUR SCUFFED MOVE CODE GOES BELOW HERE

    }
}
