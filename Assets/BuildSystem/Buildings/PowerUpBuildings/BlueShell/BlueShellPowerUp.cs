using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlueShellPowerUp : PowerUpBuilding
{
    [SerializeField] private BlueShellMissile missilePrefab;

    protected override void OnCollected(Player collectingPlayer, PlayerController collectingController)
    {
        if (missilePrefab == null)
            return;
        
        var leaders = FindPlayersInLead();
        if (leaders == null || leaders.Count == 0)
            return;
        
        foreach (var leader in leaders)
        {
            if (leader == null) continue;

            Transform ball = leader.GetBallTransform();
            if (ball == null) continue;

            Vector3 spawnPos = transform.position;
            var missile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
            
            missile.Launch(ball, Random.Range(0f, 360f));
        }
    }
    
    private List<Player> FindPlayersInLead()
    {
        var players = Resources.FindObjectsOfTypeAll<Player>().OrderBy(player => player.score).ToArray();
        var bestScore = players.Last().score;
        
        var leaders = new List<Player>();
        for (int i = players.Count() - 1; i >= 0; i--)
        {
            if (players[i].score != bestScore)
                break;
            
            leaders.Add(players[i]);
        }
        
        return leaders;
    }
}
