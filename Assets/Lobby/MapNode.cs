using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapNode : MonoBehaviour
{
    // must match the scene name of the level
    [SerializeField] private string mapName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessageUpwards("OnMapNodeVoted", mapName, SendMessageOptions.DontRequireReceiver);
    }
}