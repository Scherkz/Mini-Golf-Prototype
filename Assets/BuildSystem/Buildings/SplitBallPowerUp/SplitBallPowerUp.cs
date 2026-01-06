using UnityEngine;
using UnityEngine.InputSystem;

public class SplitBallPowerUp : PowerUpBuilding
{
    [SerializeField] private float spawnOffset = 0.4f;
    [SerializeField] private float spreadDegrees = 45f;

    protected override void OnCollected(Player player, PlayerController controller)
    {
        var playerInput = player.GetComponent<PlayerInput>();

        // Clone Player
        var cloneInput = PlayerInput.Instantiate(
            player.gameObject,
            controlScheme: "Gamepad",
            pairWithDevice: playerInput.devices[0]
        );

        // Setup Clone
        var clonePlayer = cloneInput.gameObject.AddComponent<ClonePlayer>();
        clonePlayer.CallNextFrame(clonePlayer.Setup, player, spreadDegrees);

        // Apply spread
        var playerRb = player.GetPlayerController().GetComponent<Rigidbody2D>();

        var moveDir = playerRb.linearVelocity;
        playerRb.linearVelocity = Quaternion.Euler(0, 0, spreadDegrees * 0.5f) * moveDir;

        var orthoDir = new Vector2(-moveDir.y, moveDir.x).normalized;
        player.transform.position += (Vector3) (spawnOffset * orthoDir);
        clonePlayer.transform.position -= (Vector3) (spawnOffset * orthoDir);
    }
}
