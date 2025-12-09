using System;
using UnityEngine;

public class PushAwayShot : SpecialShot
{
    private bool collisionHappenedDuringPushAwayShot = false;

    private PlayerController playerController;
    private Player player;
    private Rigidbody2D body;

    [SerializeField] float maximalImpactRange = 10f;
    [SerializeField] float maximalImpactForce = 35f;

    [SerializeField] private GameObject specializedShotVFX;
    [SerializeField] private GameObject explodeVFX;

    public override void Init(PlayerController playerController, Player player, Rigidbody2D body)
    {
        this.playerController = playerController;
        this.player = player;
        this.body = body;
        
        if(playerController != null)
        {
            playerController.BallCollisionEvent += HandleCollision;
            playerController.OnEnableSpecialShotVFX += EnableSpecialShotVFX;
            playerController.OnDisableSpecialShotVFX += DisableSpecialShotVFX;
        }

        if(player != null)
        {
            player.OnDisableSpecialShotVFX += DisableSpecialShotVFX;
        }

        currentSpecializedShotVFX = Instantiate(specializedShotVFX, playerController.transform);
        currentSpecializedShotVFX.SetActive(false);
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.BallCollisionEvent -= HandleCollision;
        }

    }

    // Handle collision for push-away shots
    private void HandleCollision(Collision2D collision)
    {
        if (playerController.HadCollisonSinceLastShot() || collisionHappenedDuringPushAwayShot) return;

        if (!playerController.IsSpecialShotEnabled()) return;

        if (this.body.linearVelocity.magnitude < playerController.GetSignificantVelocity())
            return;

        PushAwayImpact(collision);
        player.UsedSpecialShot();
        collisionHappenedDuringPushAwayShot = true;

    }

    private void PushAwayImpact(Collision2D collision)
    {
        // Impact from current player position
        Vector2 impactPosition = transform.position;
        // Get all rigidbodies in a radius
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(impactPosition, maximalImpactRange);
        foreach (Collider2D overlappingCollider in overlappingColliders)
        {
            // Only affect player rigidbodies
            if (!overlappingCollider.CompareTag("Player")) continue;

            // Apply force away from impact position
            Rigidbody2D otherBallBody = overlappingCollider.GetComponent<Rigidbody2D>();
            if (otherBallBody != null && otherBallBody != this.body)
            {
                // Only the faster ball should apply a force to the other balls if both activated push-away shots
                // Prevents applying forces in both directions
                if (otherBallBody.linearVelocity.magnitude > this.body.linearVelocity.magnitude) continue;

                Vector2 pushDirection = (otherBallBody.position - impactPosition).normalized;
                float distance = Vector2.Distance(otherBallBody.position, impactPosition);
                float maxBallSpeed = 27f;
                float t = Mathf.Clamp01(this.body.linearVelocity.magnitude / maxBallSpeed);

                float distanceFactor = 1 - Mathf.Clamp01(1/(distance / maximalImpactRange));
                //float distanceFactorPow = Mathf.Pow(distanceFactor, 2); // Square for more falloff
                t = t*0.7f + distanceFactor*0.3f;
                float forceMagnitude = Mathf.Lerp(maximalImpactForce, 0f, t);
                otherBallBody.AddForce(pushDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }

        Destroy(currentSpecializedShotVFX);
        Instantiate(explodeVFX, playerController.transform);
    }
}
