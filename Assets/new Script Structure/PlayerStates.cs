using System.Collections;
using UnityEngine;

public abstract class PlayerStates
{
    //Object references
    protected PlayerManager player;
    protected GameObject playerObj;

    //behaviour methods
    public virtual void ActionPrimary()
    {
        
    }

    public  virtual void ActionSecondary()
    {
    
    }

    public virtual void HandleMoveInput(Vector2 movement)
    {
            
        Debug.Log("Base move called");
    }

    public virtual void StopMoveInput(Vector2 movement)
    {

    }

    public virtual void Swap(Item newItem)
    {

    }

    public virtual void Move()
    {

    }

    //Logic methods
    public virtual void SwitchFrom() //Should be called on the last frame of this state being active - any state switching that needs to be done from here should be done with this method
    {

    }

    public virtual void Initiate(PlayerManager playerRef, GameObject obj) //Should be called on the frst frame of this state being active. Any seetup that needs to be done should be done from here
    {
        player = playerRef;
        playerObj = obj;
    }

    public virtual Vector2 GetMoveDirection()
    {
        return new Vector2();
    }
}
