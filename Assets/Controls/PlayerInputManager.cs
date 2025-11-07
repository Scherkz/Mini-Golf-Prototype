using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private HashSet<Gamepad> joinedGamepads = new HashSet<Gamepad>();

    void Update()
    {
        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !joinedGamepads.Contains(gamepad))
            {
                var player = PlayerInput.Instantiate(playerPrefab, 
                    controlScheme: "Gamepad", 
                    pairWithDevice: gamepad);
                player.GetComponent<Renderer>().material.color = GetRandomColor();
                joinedGamepads.Add(gamepad);
            }
        }
    }

    private static Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
