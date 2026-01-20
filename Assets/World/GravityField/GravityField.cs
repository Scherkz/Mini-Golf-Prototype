using UnityEngine;

public class GravityField : MonoBehaviour
{
    private const string TimeOffsetShaderPropertyName = "_TimeOffset";

    public float gravityFactor = 9.81f;

    private void Awake()
    {
        var radius = GetComponent<CircleCollider2D>().radius;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.StartsWith("Visualizer"))
            {
                child.transform.localScale = new Vector3(radius, radius, 1);

                var propBlock = new MaterialPropertyBlock();
                propBlock.SetFloat(TimeOffsetShaderPropertyName, Random.value);
                child.GetComponent<SpriteRenderer>().SetPropertyBlock(propBlock);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var dir = transform.position - collision.transform.position;
        var gravity = dir.normalized * gravityFactor / dir.sqrMagnitude;
        collision.attachedRigidbody.AddForce(gravity, ForceMode2D.Force);
    }
}
