using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer rend;
    private PlayerManager pm;

    private int nodeType = 0;
    private int health = 100;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("GM").GetComponent<PlayerManager>();
    }

    public void ChangeType(int type)
    {
        nodeType = type;
        health += type * 10;
        rend.sprite = sprites[type];
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GiveResources();
        }
    }

    private void GiveResources()
    {
        System.Random randomSeed = new System.Random(Time.time.ToString().GetHashCode());

        switch (nodeType)
        {
            case 0:
                pm.UpdateResources(ResourceDefinitions.GemRuby, randomSeed.Next(0, 4));
                break;
            case 1:
                pm.UpdateResources(ResourceDefinitions.GemDiamond, randomSeed.Next(0, 4));
                break;
            case 2:
                pm.UpdateResources(ResourceDefinitions.GemSapphire, randomSeed.Next(0, 4));
                break;
            case 3:
                pm.UpdateResources(ResourceDefinitions.GemAmethyst, randomSeed.Next(0, 4));
                break;
            case 4:
                pm.UpdateResources(ResourceDefinitions.GemPearl, randomSeed.Next(0, 4));
                break;
            case 5:
                pm.UpdateResources(ResourceDefinitions.GemGold, randomSeed.Next(0, 4));
                break;
            case 6:
                pm.UpdateResources(ResourceDefinitions.GemErrol, randomSeed.Next(0, 4));
                break;
        }
    }

}
