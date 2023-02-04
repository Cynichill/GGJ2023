using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftLeft : MonoBehaviour
{
    private CameraMove move;

    private void Start()
    {
        move = transform.parent.GetComponent<CameraMove>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            move.TriggerHit(false);
        }
    }
}
