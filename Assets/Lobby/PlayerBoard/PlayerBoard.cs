using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LobbyManager;

public class PlayerBoard : MonoBehaviour
{
    [SerializeField] private GameObject playerBoardEntryPrefab;
    private readonly Dictionary<Player, PlayerBoardEntry> entries = new();
    private LobbyManager lobbyManager = null;

    private void OnEnable()
    {
        EventBus.Instance.OnPlayerJoined += AddPlayerEntry;
        EventBus.Instance.OnPlayerLeft += RemovePlayerEntry;
        EventBus.Instance.OnMapVoted += OnPlayerVoted;
        EventBus.Instance.OnPlayerReady += OnPlayerReady;
    }

    private void OnDisable()
    {
        if (EventBus.Instance == null) return;

        EventBus.Instance.OnPlayerJoined -= AddPlayerEntry;
        EventBus.Instance.OnPlayerLeft -= RemovePlayerEntry;
        EventBus.Instance.OnMapVoted -= OnPlayerVoted;
        EventBus.Instance.OnPlayerReady -= OnPlayerReady;
    }

    private void Start()
    {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        lobbyManager = FindFirstObjectByType<LobbyManager>();

        foreach (var p in players)
        {
            AddPlayerEntry(p);
        }
    }

    private void AddPlayerEntry(Player player)
    {
        if (entries.ContainsKey(player)) return;

        var entryPrefab = Instantiate(playerBoardEntryPrefab, transform);
        var entry = entryPrefab.GetComponent<PlayerBoardEntry>();

        entries[player] = entry;

        var lp = lobbyManager.GetLobbyPlayer(player);
        entry.SetPlayerColor(lp.player.GetColor());
        entry.SetPlayerName(lp.playerID);
        entry.SetReady(lp.ready);
        entry.SetMapVote(lp.mapVote?.mapName);

        RebuildEntryPositions();
    }

    private void RemovePlayerEntry(Player player)
    {
        if (!entries.ContainsKey(player)) return;

        Destroy(entries[player].gameObject);
        entries.Remove(player);

        RebuildEntryPositions();
    }

    private void OnPlayerVoted(MapNode map, Player player)
    {
        if (!entries.ContainsKey(player)) return;

        entries[player].SetMapVote(map.mapName);
    }

    private void OnPlayerReady(Player player)
    {
        if (!entries.ContainsKey(player)) return;

        var lp = lobbyManager.GetLobbyPlayer(player);
        entries[player].SetReady(lp.ready);
    }

    private void RebuildEntryPositions()
    {
        int index = 0;

        foreach (var pair in entries.OrderBy(e => lobbyManager.GetLobbyPlayer(e.Key).playerID))
        {
            var entry = pair.Value;
            var rect = entry.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(0, -index * 32f);

            var lobbyPlayer = lobbyManager.GetLobbyPlayer(pair.Key);
            lobbyPlayer.playerID = index;

            entry.SetPlayerName(index);

            index++;
        }
    }


}
