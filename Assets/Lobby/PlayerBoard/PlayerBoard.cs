using System.Collections.Generic;
using UnityEngine;

public class PlayerBoard : MonoBehaviour
{
    [SerializeField] private GameObject playerBoardEntryPrefab;
    private readonly Dictionary<Player, PlayerBoardEntry> entries = new();

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

        entry.SetPlayerColor(player.GetColor());
        entry.SetMapVote(null);
    }

    private void RemovePlayerEntry(Player player)
    {
        if (!entries.ContainsKey(player)) return;

        Destroy(entries[player].gameObject);
        entries.Remove(player);
    }

    private void OnPlayerVoted(string mapName, Player player)
    {
        if (!entries.ContainsKey(player)) return;

        entries[player].SetMapVote(mapName);
    }

    private void OnPlayerReady(Player player)
    {
        if (!entries.ContainsKey(player)) return;

         entries[player].ToggleReady();
    }


}
