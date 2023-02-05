using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemAxe : Item
{

    private float cooldown = 0.5f;
    private Vector2 hitLocation;
    private IChoppable target;

    public override void Initialise()
    {
        base.Initialise();
    }

    public override void Swap()
    {
        base.Swap();
    }

    public override void Use(PlayerManager usingPlayer)
    {
        base.Use(usingPlayer);
        
        //Casts rays left or right dependant on wether or not 
        Ray2D hitRay = new Ray2D(new Vector2(usingPlayer.GetGameObject().transform.position.x, usingPlayer.GetGameObject().transform.position.y), Vector2.ClampMagnitude(usingPlayer.GetMovement(), 1f));
       
        RaycastHit2D[] hits = Physics2D.RaycastAll(hitRay.origin, hitRay.direction);
        Debug.DrawRay(hitRay.origin, hitRay.direction, Color.blue, 10000.0f, false);

        foreach (RaycastHit2D hit in hits)
        {
            //Guard clause against not choppable
            if (hit.collider.gameObject.GetComponent<IChoppable>() == null)
            {
                continue;
            }
            target = hit.collider.gameObject.GetComponent<IChoppable>();
            hitLocation = hitRay.direction * hit.distance;

        }

        //Guard clause against no target
        if (target != null)
        {
            target.Chop(hitRay.origin + hitLocation);
        }

         
    }
}
