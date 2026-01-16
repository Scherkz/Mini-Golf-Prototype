using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravityFactor = 9.81f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var dir = transform.position - collision.transform.position;
        var gravity = dir.normalized * gravityFactor / dir.sqrMagnitude;
        collision.attachedRigidbody.AddForce(gravity, ForceMode2D.Force);
    }
}
