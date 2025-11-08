using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private HashSet<Gamepad> joinedGamepads = new HashSet<Gamepad>();
    private int playerCount = 0;

    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count && i < spawnPoints.Length; i++)
        {
            var gamepad = Gamepad.all[i];
            SpawnPlayer(gamepad, spawnPoints[i].position);
        }
    }
    private void SpawnPlayer(Gamepad gamepad, Vector3 spawnPosition)
    {
        playerCount++;

        var player = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: "Gamepad",
            pairWithDevice: gamepad
        );

        player.transform.position = spawnPosition;
        player.name = $"PlayerBall {playerCount}";
        player.GetComponent<Renderer>().material.color = GetRandomColor();
        joinedGamepads.Add(gamepad);
    }

    private static Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
