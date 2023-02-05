using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public enum ResourceDefinitions
{
    Wood,
    Stone,
    GemRuby,
    GemDiamond,
    GemSapphire,
    GemAmethyst,
    GemPearl,
    GemGold,
    GemErrol
}

public struct GemTypes
{
    public int ruby;
    public int diamond;
    public int sapphire;
    public int amethyst;
    public int pearl;
    public int gold;
    public int errol;
}

public struct Resources
{
    public int wood;
    public int stone;
    public GemTypes gems;
}

public class PlayerManager : MonoBehaviour
{
    //Data trackers
    private bool enabled = true; //Selectivley enables and disables the player in update.

    public Vector2 currentMovement
    {
        get
        {
            return GetPlayerMovement();
        }
    }

    //Reference to active player state
    private PlayerStates playerState;

    //Publicly accessible resource list
    public Resources playerResources;

    public GameObject playerObj;

    //private inputsystem reference
    private PlayerControl controls;
    private void Awake()
    {
        controls = new PlayerControl();
        controls.Player.Interact.performed += ctx => playerState.ActionPrimary();
        controls.Player.Move.performed += ctx => playerState.HandleMoveInput(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => playerState.StopMoveInput(ctx.ReadValue<Vector2>());

        //Setup player transform reference
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }


        playerState = new PlayerAttackState();
        playerState.Initiate(this, playerObj); //Initialise playerstate
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private Vector2 GetPlayerMovement()
    {
        return playerState.GetMoveDirection();
    }
    //methods

    //Public

    public void HandlePlayerActions()
    {
        //guard clause
        //if the player is disabled, do not run any subclass logic. Treat this as a "not active"

        if (!enabled)
        {
            return;
        }

        playerState.Move();

    }


    //Handles resource transactions of single material
    public bool SpendResource(ResourceDefinitions resource, int amount)
    {
        //Operate through switch statements for each resource type, if type has enough availible resources, return true and spend the resources
        if (ValidateTransaction(resource, amount))
        {
            UpdateResources(resource, -amount);
            return true;
        } else
        {
            return false;
        }
    }

    //Handles resource transactions of multiple material
    public bool SpendResources(ResourceDefinitions[] resource, int[] amount)
    {
        //Guard clause against invalid transaction
        for (int i = 0; i < resource.Length; i++) 
        {
            if (!ValidateTransaction(resource[i], amount[i]))
            {
                return false;
            }
        }

        //Effect transaction 
        for (int i =0; i < amount.Length; i++)
        {
            UpdateResources(resource[i], amount[i]);
        }

        return true;
    }


    //Handles state switching when attack mode or setup mode is called
    public void SwitchState(PlayerStates newState)
    {
        playerState.SwitchFrom();
        playerState = newState;
        playerState.Initiate(this, playerObj);
    }

    public GameObject GetGameObject()
    {
        return playerObj;
    }

    //private
    //helpers

    //Updates resource ammounts
    private void UpdateResources(ResourceDefinitions resource, int amount)
    {
        switch (resource)
        {
            case ResourceDefinitions.Wood:
                playerResources.wood += amount;
                break;

            case ResourceDefinitions.Stone: 
                playerResources.stone += amount;
                break;

            case ResourceDefinitions.GemRuby:
                playerResources.gems.ruby += amount;
                break;

            case ResourceDefinitions.GemDiamond:
                playerResources.gems.diamond += amount;
                break;

            case ResourceDefinitions.GemSapphire:
                playerResources.gems.sapphire += amount;
                break;

            case ResourceDefinitions.GemAmethyst:
                playerResources.gems.amethyst += amount;
                break;

            case ResourceDefinitions.GemPearl:
                playerResources.gems.pearl += amount;
                break;

            case ResourceDefinitions.GemGold:
                playerResources.gems.gold += amount;
                break;

            case ResourceDefinitions.GemErrol:
                playerResources.gems.errol += amount;
                break;
        }
    }

    //Validates quantity of resource against resource definition enum
    private bool ValidateTransaction(ResourceDefinitions resource, int amount)
    {
        switch (resource)
        {
            case ResourceDefinitions.Wood:
                if (playerResources.wood >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.Stone:
                if (playerResources.stone >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemPearl:
                if (playerResources.gems.pearl >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemGold:
                if (playerResources.gems.gold >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemErrol:
                if (playerResources.gems.errol >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemAmethyst:
                if (playerResources.gems.amethyst >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemSapphire:
                if (playerResources.gems.sapphire >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemDiamond:
                if (playerResources.gems.diamond >= amount)
                {
                    return true;
                }
                else return false;

            case ResourceDefinitions.GemRuby:
                if (playerResources.gems.ruby >= amount)
                {
                    return true;
                }
                else return false;
        }

        //else...
        return false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
