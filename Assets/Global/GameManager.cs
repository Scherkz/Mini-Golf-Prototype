using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInfo[] players;

    enum Phase
    {
        Building,
        Playing,
    }

    private Phase currentPhase;

    private void OnEnable()
    {
        EventBus.Instance.OnPlayerPlacedBuilding += OnPlayerPlacesBuilding;
        EventBus.Instance.OnPlayerFinishedRound += OnPlayerFinishedRound;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnPlayerPlacedBuilding += OnPlayerPlacesBuilding;
        EventBus.Instance.OnPlayerFinishedRound += OnPlayerFinishedRound;
    }

    private void Start()
    {
        // TODO: remove onyl for testing
        var players = new PlayerInfo[2];
        StartRound(players);
    }

    public void StartRound(PlayerInfo[] players)
    {
        this.players = players;

        StartBuildingPhase();
    }

    private void StartBuildingPhase()
    {
        currentPhase = Phase.Building;
        
        // TODO: spawn build players
        // TODO: activate build system
    }

    private void OnPlayerPlacesBuilding(PlayerInfo _playerInfo)
    {
        if (currentPhase != Phase.Building)
            return;

        foreach (var player in players)
        {
            if (!player.hasPlacedBuilding)
                return;
        }

        // all players finsihed placing their building
        // TODO: delay by one frame
        StartPlayingPhase();
    }

    private void StartPlayingPhase()
    {
        currentPhase = Phase.Building;

        // TODO: spawn players
        // TODO: activate player controls
    }

    private void OnPlayerFinishedRound(PlayerInfo _playerInfo)
    {
        if (currentPhase != Phase.Playing)
            return;

        foreach (var player in players)
        {
            if (!player.hasFinishedRound)
                return;
        }

        // all players finsihed the round
        // TODO: delay by one frame
        // TODO: check if max rounds are played
        StartBuildingPhase();
    }
}
