using System.Collections;
using UnityEngine;

public class ItemAxe : Item
{
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void Swap()
    {
        base.Swap();
    }

    public override void Use()
    {
        base.Use();
        Debug.Log("Vibe");
    }
}
