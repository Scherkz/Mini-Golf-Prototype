using UnityEngine;

public class ClonePlayer : MonoBehaviour
{
    private Player original;
    private Player clone;

    public void Setup(Player player)
    {
        original = player;
        clone = GetComponent<Player>();

        original.OnFinishedRound += OnPlayerFinishedRound;
        clone.OnFinishedRound += OnCloneFinishedRound;

        transform.SetPositionAndRotation(original.transform.position, original.transform.rotation);

        var originalController = original.GetPlayerController();
        var cloneController = clone.GetPlayerController();
        cloneController.SetResetOnStart(false);
        cloneController.SetColor(original.GetColor());

        cloneController.transform.SetPositionAndRotation(originalController.transform.position, originalController.transform.rotation);
        clone.StartPlayingPhase(originalController.transform.position);
    }

    private void OnDestroy()
    {
        original.OnFinishedRound -= OnPlayerFinishedRound;
    }

    private void OnPlayerFinishedRound()
    {
        Destroy(gameObject);
    }

    private void OnCloneFinishedRound()
    {
        original.SendMessage("OnEnterFinishArea");
        Destroy(gameObject);
    }
}
