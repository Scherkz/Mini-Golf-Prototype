using UnityEngine;
using System.Collections;

public class CannonBuilding : Building
{
    [SerializeField] private float shootPower = 15f;

    private bool isFree;
    private Coroutine shootCoroutine;

    private Collider2D myCollider;
    private Collider2D otherCollider;
    private GameObject playerGameObject;

    private Animator animator;

    private void Awake()
    {
        isFree = true;
        myCollider = GetComponentInChildren<Collider2D>();

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (isFree)
        {
            playerGameObject = collision.gameObject;
            isFree = false;
            otherCollider = collision.collider;
            Physics2D.IgnoreCollision(myCollider, otherCollider, true);
            PlayerController playerBall = playerGameObject.GetComponent<PlayerController>();
            if (playerBall != null)
                playerBall.FreezePlayerControlls();
            Vector2 midPoint = transform.position;
            midPoint.x += 0.5f;
            midPoint.y += 0.5f;
            playerGameObject.transform.position = midPoint;
            shootCoroutine = StartCoroutine(ShootAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerGameObject)
        {
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
        }
    }

    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Shoot");
    }

    // Gets triggered by animator for timing with keyframes
    private void Shoot()
    {
        PlayerController playerBall = playerGameObject.GetComponent<PlayerController>();
        if (playerBall != null)
            playerBall.DefreezePlayerControlls();
        Rigidbody2D rb = playerGameObject.GetComponent<Rigidbody2D>();

        Vector2 dir = Quaternion.Euler(0, 0, -45f) * rotationAnchor.transform.up;
        rb.linearVelocity = dir * shootPower;
    }

    private void Update()
    {
        if (isFree) return;

        Vector2 posA = transform.position;
        Vector2 posB = playerGameObject.transform.position;
        float distance = Vector2.Distance(posA, posB);
        if (distance > 2f)
        {
            Physics2D.IgnoreCollision(myCollider, otherCollider, false);
            isFree = true;
            playerGameObject = null;
        }
    }
}