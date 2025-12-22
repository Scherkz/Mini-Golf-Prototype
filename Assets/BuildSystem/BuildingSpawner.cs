using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject buildingGhostPrefab;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float maxAntiBuildingChance = 0.5f;

    private Transform anchor;
    private BuildGrid currentGrid;

    public void SpawnBuildings(BuildingData[] buildings, int buildingCount, BuildGrid grid)
    {
        // center anchor on the screen
        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        var worldPos = Camera.main.ScreenToWorldPoint(screenCenter);
        worldPos.z = 0;
        anchor.position = worldPos;
        currentGrid = grid;

        EnsureEnoughBuildingsGhosts(buildingCount);

        var angleStep = 2.0f * Mathf.PI / buildingCount;
        for (var i = 0; i < buildingCount; i++)
        {
            var buildingGhost = anchor.GetChild(i).GetComponent<BuildingGhost>();

            var angle = i * angleStep;
            var circlePos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            buildingGhost.transform.localPosition = circlePos;

            var randomBuildingData = GetRandomBuildingData(buildings);
            buildingGhost.ShowBuilding(randomBuildingData, true);
        }
    }

    private BuildingData GetRandomBuildingData(BuildingData[] buildings)
    {
        BuildingData[] realBuildings = System.Array.FindAll(buildings, b => !b.isAntiBuilding);
        BuildingData[] antiBuildings = System.Array.FindAll(buildings, b => b.isAntiBuilding);

        int buildingsOnMap = currentGrid.GetBuildingCount();
        float antiBuildingChance = Mathf.Min(buildingsOnMap * 0.1f, maxAntiBuildingChance); // Caps at maxAntiBuildingChance

        if (Random.value < antiBuildingChance)
        {
            return antiBuildings[Random.Range(0, antiBuildings.Length)];
        }
        return realBuildings[Random.Range(0, realBuildings.Length)];

    }

    private void Awake()
    {
        anchor = transform.Find("Anchor");
    }

    private void Update()
    {
        anchor.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        for (int i = 0; i < anchor.childCount; i++)
        {
            anchor.GetChild(i).transform.rotation = Quaternion.identity;
        }
    }

    private void EnsureEnoughBuildingsGhosts(int buildingCount)
    {
        if (anchor.childCount > buildingCount)
        {
            for (var i = anchor.childCount - 1; i >= buildingCount; i--)
            {
                Destroy(anchor.GetChild(i).gameObject);
            }
        }
        else if (anchor.childCount < buildingCount)
        {
            for (var i = anchor.childCount; i < buildingCount; i++)
            {
                Instantiate(buildingGhostPrefab, anchor);
            }
        }

        for (int i = 0; i < anchor.childCount; i++)
        {
            anchor.GetChild(i).gameObject.SetActive(true);
        }
    }
}