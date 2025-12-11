using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUpBuilding : Building
{
    public bool hideOnPickupInsteadOfDestroy = false;
    
    public GameObject collectVfxPrefab;
    
    private bool isCollected;
    
    private SpriteRenderer[] renderers;
    private Collider2D[] colliders;
    
    private PlayerController playerController;
    
    protected virtual void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
        colliders = GetComponentsInChildren<Collider2D>(true);

        foreach (var col in colliders)
        {
            col.isTrigger = true;
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (isCollected)
            return;
        
        if (!IsValidCollector(playerCollider, out GameObject playerPowerUp))
            return;
        
        isCollected = true;

        if (collectVfxPrefab != null)
        {
            Instantiate(collectVfxPrefab, transform.position, Quaternion.identity);
        }

        OnCollected(playerPowerUp);

        if (hideOnPickupInsteadOfDestroy)
        {
            SetVisible(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //to see if the collector is an actual player and not something else with a collider
    protected virtual bool IsValidCollector(Collider2D otherCollider, out GameObject playerPowerUp)
    {
        playerPowerUp = null;

        if (otherCollider.CompareTag("Player"))
        {
            playerPowerUp = otherCollider.gameObject;
            return true;
        }

        if (playerController != null)
        {
            playerPowerUp = playerController.gameObject;
            return true;
        }

        return false;
    }
    
    //should be overwritten by each power up
    protected abstract void OnCollected(GameObject collectingPlayer);
    
    //activates or deactivates all the collider and sprites 
    private void SetVisible(bool value)
    {
        if (renderers != null)
        {
            foreach (var rend in renderers)
            {
                rend.enabled = value;
            }
        }

        if (colliders != null)
        {
            foreach (var col in colliders)
            {
                col.enabled = value;
            }
        }
    }


    //To activate in GameManager, to reset all the hidden power-ups
    public virtual void ResetForNextRound()
    {
        isCollected = false;
        SetVisible(true);
    }
}
