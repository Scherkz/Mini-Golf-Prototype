using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData data;

    private GameObject prefabInstance;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (data != null)
        {
            Init(data);
        }
    }

    public void Init(BuildingData building)
    {
        if (prefabInstance != null)
        {
            Destroy(prefabInstance);
            prefabInstance = null;
            spriteRenderer = null;
        }

        data = building;

        prefabInstance = Instantiate(data.prefab);
        prefabInstance.transform.SetParent(transform, false);
        prefabInstance.name = data.name;
        spriteRenderer = prefabInstance.GetComponent<SpriteRenderer>();
    }

    public void SetTint(Color tint)
    {
        spriteRenderer.material.color = tint;
    }
}
