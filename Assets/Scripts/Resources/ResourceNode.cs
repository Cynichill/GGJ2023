using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer rend;

    private int nodeType = 0;
    
    public void ChangeType(int type)
    {
        nodeType = type;
        rend.sprite = sprites[type];
    }

    private void GiveResources()
    {

    }

}
