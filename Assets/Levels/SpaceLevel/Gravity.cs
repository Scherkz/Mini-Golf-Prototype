using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Gravity : MonoBehaviour
{
    private float gravityStrength = 50f;
    private float gravityRange = 10f;
    private float extraRadius = 2f;
    private bool useDistanceFalloff = true;

    private readonly List<Rigidbody2D> players = new List<Rigidbody2D>();

    public Color gizmoColor = new Color(0f, 0.5f, 1f, 0.25f);
    public bool showGizmoInGame = true;


    private void OnValidate()
    {
        UpdateGravityRange();
    }

    private void Reset()
    {
        UpdateGravityRange();
    }

    private void UpdateGravityRange()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // Calculate radius as half of largest dimension + extraRadius
            gravityRange = Mathf.Max(sr.bounds.size.x, sr.bounds.size.y) / 2f + extraRadius;
        }
    }

    private void FixedUpdate()
    {
        RefreshPlayerListIfNeeded();

        for (int i = players.Count - 1; i >= 0; i--)
        {
            Rigidbody2D rb = players[i];

            if (rb == null)
            {
                players.RemoveAt(i);
                continue;
            }

            Vector2 direction = (Vector2)transform.position - rb.position;
            float distance = direction.magnitude;

            if (distance > gravityRange)
                continue;

            direction.Normalize();

            float force = gravityStrength;

            if (useDistanceFalloff)
            {
                force *= 1f - (distance / gravityRange);
            }

            rb.AddForce(direction * force, ForceMode2D.Force);
        }
    }

    private void RefreshPlayerListIfNeeded()
    {
        // Only refresh if list is empty or contains nulls
        if (players.Count > 0 && !players.Contains(null))
            return;

        players.Clear();

        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in foundPlayers)
        {
            Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
            if (rb != null)
                players.Add(rb);
        }
    }
    private void OnDrawGizmos()
    {
        if (!showGizmoInGame && Application.isPlaying)
            return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gravityRange);
    }
}
