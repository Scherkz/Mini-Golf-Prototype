using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private float rotationSpeed = 30f;

    private Transform anchor;
    
    public void SpawnBuildings(BuildingData[] buildings, int buildingCount)
    {
        if (buildings == null || buildings.Length == 0) 
            return;
        
        var angleStep = 2.0f * Mathf.PI / buildingCount;

        for (var i = 0; i < buildingCount; i++)
        {
            var randomBuildingData = buildings[Random.Range(0, buildings.Length)];
            var build = Instantiate(randomBuildingData.prefab, transform);
            build.transform.SetParent(anchor);
            
            var angle = i * angleStep;
            var circlePos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            build.transform.localPosition = circlePos;
        }
    }

    private void Awake()
    {
        anchor = transform.Find("Anchor");

        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        var worldPos = Camera.main.ScreenToWorldPoint(screenCenter);
        worldPos.z = 0;
        
        anchor.position = worldPos;
    }

    private void Update()
    {
        anchor.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        for (int i = 0; i < anchor.childCount; i++)
        {
            anchor.GetChild(i).transform.rotation = Quaternion.identity;
        }
    }
}