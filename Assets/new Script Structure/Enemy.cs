using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IChoppable, IBurnable
{
    public virtual void Chop()
    {

    }

    public void Burn()
    {

    }

    protected virtual void Move()
    {

    } 
}
