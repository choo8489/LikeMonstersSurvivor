using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private RuntimeAnimatorController[] animatorController;

    private Rigidbody2D target;

    private bool isLive;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private void OnEnable()
    {
        target = GameManager.instance.Player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
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

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
