using UnityEngine;

public class PlanetClouds : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;

    private float loopOffset;

    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        loopOffset = spriteRenderer.size.x * 0.5f;
    }

    private void Update()
    {
        float xOffset = Mathf.Repeat(Time.time * scrollSpeed, loopOffset);
        float x = -(loopOffset * 0.5f) + xOffset;
        transform.position = new Vector3(x, 0, 0);
    }
}
