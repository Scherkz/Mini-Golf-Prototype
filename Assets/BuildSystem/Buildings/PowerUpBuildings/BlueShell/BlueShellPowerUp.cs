using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlueShellPowerUp : PowerUpBuilding
{
    
    [SerializeField] private BlueShellMissile missilePrefab;
    
    [SerializeField] private Vector3 missileSpawnOffset = new Vector3(0f, 0.1f, 0f);

    private Player[] players = { };
    protected override void OnCollected(GameObject collectingObject)
    {
        if (missilePrefab == null)
            return;
        
        Transform[] leaders = FindPlayersInLead();
        if (leaders == null || leaders.Length == 0)
            return;
        
        foreach (Transform target in leaders)
        {
            if (target == null)
                continue;

            Vector3 spawnPos = transform.position + missileSpawnOffset;
            BlueShellMissile missile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);

            float randomAngle = Random.Range(0f, 360f);
            missile.Launch(target, randomAngle);
        }
    }
    
    private Transform[] FindPlayersInLead()
    {
        List<Transform> leaders = new List<Transform>();
        
        players = players.OrderBy(player => player.score).ToArray();
        
        var bestScore = players[players.Length - 1].score;
        
        foreach (var p in players)
        {
            if (p.score == bestScore)
                leaders.Add(p.transform);
        }

        return leaders.ToArray();
    }
    
}
