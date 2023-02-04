using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillHealth : MonoBehaviour
{
    [SerializeField] private int health = 50;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Root")
        {
            TakeDamage(1);
        } 
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Drill broken!");
        }
    }
}
