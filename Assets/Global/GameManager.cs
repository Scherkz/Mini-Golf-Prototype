using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players;

    [SerializeField] private Transform spawnPointsParents;

    enum Phase
    {
        Building,
        Playing,
    }

    private Phase currentPhase;

    private void OnEnable()
    {
        EventBus.Instance.OnStartGame -= StartRound;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnStartGame += StartRound;
    }

    public void StartRound(Player[] players)
    {
        this.players = players;

        foreach (var player in players)
        {
            player.OnPlacedBuilding += OnPlayerPlacesBuilding;
            player.OnFinishedRound += OnPlayerFinishedRound;
        }

        StartBuildingPhase();
    }

    private void StartBuildingPhase()
    {
        currentPhase = Phase.Building;
        
        // TODO: spawn build players
        // TODO: activate build system
    }

    private void OnPlayerPlacesBuilding()
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

    private void OnPlayerFinishedRound()
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
