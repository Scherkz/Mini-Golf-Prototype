using UnityEngine;

public class BoostPad : MonoBehaviour
{
    private float boostMultiplier = 3.0f;
    private bool preserveY = true; // set true if you only want horizontal boost

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the player
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null)
            return;

        Vector2 velocity = rb.linearVelocity;

        if (preserveY)
        {
            velocity.x *= boostMultiplier;
        }
        else
        {
            velocity *= boostMultiplier;
        }
        Debug.Log("Apply Boost");
        rb.linearVelocity = velocity;
    }
}