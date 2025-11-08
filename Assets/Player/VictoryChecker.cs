using UnityEngine;

public class VictoryChecker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("VictoryZone"))
        {
            Debug.Log($"{name} touched the VictoryZone!");
        }
    }
}
