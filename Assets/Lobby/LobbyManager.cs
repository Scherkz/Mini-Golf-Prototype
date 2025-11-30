using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;

    private readonly List<LobbyPlayer> players = new();

    public record LobbyPlayer
    {
        public Player player;
        public int playerID;
        public bool ready;
        public string mapVote;
    }

    private void OnEnable()
    {
        if (EventBus.Instance == null) return;
        EventBus.Instance.OnPlayerJoined += OnPlayerJoined;
        EventBus.Instance.OnPlayerLeft += OnPlayerLeft;
        EventBus.Instance.OnMapVoted += OnMapVoted;
    }

    private void OnDisable()
    {
        if (EventBus.Instance == null) return;
        EventBus.Instance.OnPlayerJoined -= OnPlayerJoined;
        EventBus.Instance.OnPlayerLeft -= OnPlayerLeft;
        EventBus.Instance.OnMapVoted -= OnMapVoted;
    }

    void Start()
    {
        if(playerSpawner == null) { playerSpawner = FindFirstObjectByType<PlayerSpawner>(); }

        foreach (var player in playerSpawner.GetAllPlayers())
        {
            RegisterPlayer(player);
        }

        playerSpawner.allowJoining = true;
    }

    private void Update()
    {
        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                var player = playerSpawner.GetPlayerByGamepad(gamepad);
                if (player == null) continue;

                TogglePlayerReady(player);
            }
        }
    }

    private void TogglePlayerReady(Player player)
    {
        var lobbyPlayer = players.Find(s => s.player == player);
        if (lobbyPlayer == null) return;

        lobbyPlayer.ready = !lobbyPlayer.ready;

        EventBus.Instance?.OnPlayerReady?.Invoke(player);

        if (lobbyPlayer.ready)
        {
            TryStartingLevel();
        }

    }

    private void OnPlayerJoined(Player player)
    {
        RegisterPlayer(player);
    }

    private void OnPlayerLeft(Player player)
    {
        var lobbyPlayer = players.Find(lp => lp.player == player);
        if (lobbyPlayer != null)
        {
            players.Remove(lobbyPlayer);
        }
    }

    private void RegisterPlayer(Player player)
    {
        if (players.Any(lobbyPlayer => lobbyPlayer.player == player)) return;

        var lobbyPlayer = new LobbyPlayer()
        {
            player = player,
            ready = false,
            mapVote = null
        };

        players.Add(lobbyPlayer);

        Debug.Log($"Player {lobbyPlayer.playerID} registered.");
    }

    private void OnMapVoted(string mapName, Player player)
    {
        var lobbyPlayer = players.Find(lobbyPlayer => lobbyPlayer.player == player);
        
        if (lobbyPlayer == null) return;

        lobbyPlayer.mapVote = mapName;

        Debug.Log($"Player map vote: {lobbyPlayer.mapVote}");

        TryStartingLevel();
    }

    private void TryStartingLevel()
    {
        if (!players.Any()) return;

        if (players.Any(s => string.IsNullOrEmpty(s.mapVote) || !s.ready))
            return;

        string winning = PickWinningMap(players.Select(s => s.mapVote).ToList());
        playerSpawner.allowJoining = false;

        Debug.Log($"LobbyManager selected map: {winning}");
        EventBus.Instance?.OnMapSelected?.Invoke(winning);
    }

    private string PickWinningMap(List<string> votes)
    {
        var groups = votes.GroupBy(v => v).Select(g => new { MapName = g.Key, Count = g.Count() }).ToList();
        int max = groups.Max(g => g.Count);
        var top = groups.Where(g => g.Count == max).ToList();

        if (top.Count == 1) return top[0].MapName;

        int r = UnityEngine.Random.Range(0, top.Count);
        return top[r].MapName;
    }

    public LobbyPlayer GetLobbyPlayer(Player player)
    {
        return players.Find(lp => lp.player == player);
    }

}
