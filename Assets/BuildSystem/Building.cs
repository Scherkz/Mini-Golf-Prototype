using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    public BuildingData data;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (data != null)
        {
            Init(data);
        }
    }

    public void Init(BuildingData building)
    {
        data = building;

        name = data.name;
        spriteRenderer.sprite = data.sprite;
    }

    public void SetTint(Color tint)
    {
        spriteRenderer.material.color = tint;
    }
}
