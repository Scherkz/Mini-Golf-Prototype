using UnityEngine;
using UnityEngine.InputSystem;

public class SplitBallPowerUp : PowerUpBuilding
{
    [SerializeField] private Vector2 cloneSpawnOffset = new(0.6f, 0f);
    [SerializeField] private float spreadDegrees = 45f;

    protected override void OnCollected(Player player, PlayerController controller)
    {
        //var originalBall = controller.gameObject;
        //
        //var originalRb = originalBall.GetComponent<Rigidbody2D>();
        //var snapLinearVel = originalRb != null ? originalRb.linearVelocity : Vector2.zero;
        //var snapAngularVel = originalRb != null ? originalRb.angularVelocity : 0f;
        //
        //var clone = Instantiate(originalBall, originalBall.transform.parent);
        //clone.name = originalBall.name + "_Clone";
        //clone.transform.SetPositionAndRotation(originalBall.transform.position + (Vector3)cloneSpawnOffset, originalBall.transform.rotation);
        //
        //var cloneController = clone.GetComponent<PlayerController>();
        //cloneController.SetResetOnStart(false);          
        //cloneController.SetColor(player.GetColor());     
        //    
        //var cloneRb = clone.GetComponent<Rigidbody2D>();
        //if (cloneRb != null)
        //{
        //    var speed = snapLinearVel.magnitude;
        //    var dir = snapLinearVel / speed;
        //
        //    var spreadDir = (Vector2)(Quaternion.Euler(0f, 0f, spreadDegrees) * dir);
        //
        //    var perp = new Vector2(-dir.y, dir.x);
        //    var side = Mathf.Sign(spreadDegrees);
        //    clone.transform.position = originalBall.transform.position + (Vector3)(perp * cloneSpawnOffset * side);
        //
        //    cloneRb.linearVelocity = spreadDir * speed;
        //    cloneRb.angularVelocity = snapAngularVel;
        //    cloneRb.WakeUp();
        //}

        var playerInput = player.GetComponent<PlayerInput>();

        // Clone Player
        var cloneInput = PlayerInput.Instantiate(
            player.gameObject,
            controlScheme: "Gamepad",
            pairWithDevice: playerInput.devices[0]
        );

        // Setup Clone
        var clonePlayer = cloneInput.gameObject.AddComponent<ClonePlayer>();
        clonePlayer.CallNextFrame(clonePlayer.Setup, player);
    }
}
