using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D target;

    private bool isLive = true;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = target.position.x < rigidbody.position.x;
    }
}
