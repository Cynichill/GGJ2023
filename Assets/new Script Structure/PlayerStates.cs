using System.Collections;
using UnityEngine;

public abstract class PlayerStates
{
    //Object references
    private PlayerManager player;

    //behaviour methods
    public virtual void ActionPrimary()
    {
        
    }

    protected virtual void ActionSecondary()
    {
    
    }

    public virtual void Move(Vector2 movement)
    {

    }

    //Logic methods
    public virtual void SwitchFrom() //Should be called on the last frame of this state being active - any state switching that needs to be done from here should be done with this method
    {

    }

    public virtual void Initiate(PlayerManager playerRef) //Should be called on the frst frame of this state being active. Any seetup that needs to be done should be done from here
    {
        player = playerRef;
    }
}
