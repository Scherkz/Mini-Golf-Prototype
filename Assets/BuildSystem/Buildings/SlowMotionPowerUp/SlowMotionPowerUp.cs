using System.Collections.Generic;
using UnityEngine;

public class SlowMotionPowerUp : PowerUpBuilding
{

    [SerializeField] private PlayerRegistry playerRegistry;

    protected override void OnCollected(Player collectingPlayer, PlayerController collectingController)
    {
        // debug statement
        Debug.Log("Slow Motion Power Up Collected by Player: " + collectingPlayer.name);
    }
}
