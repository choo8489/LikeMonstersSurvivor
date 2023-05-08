using System.Collections;
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
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private WaitForFixedUpdate wait;

    private void Awake()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out collider);

        wait= new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        target = GameManager.instance.Player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

        collider.enabled = true;
        rigidbody.simulated = true;
        animator.SetBool("Dead", false);

        spriteRenderer.sortingOrder += 1;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        spriteRenderer.flipX = target.position.x < rigidbody.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            collider.enabled = false;
            rigidbody.simulated = false;
            spriteRenderer.sortingOrder -= 1;
            animator.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private IEnumerator KnockBack()
    {
        yield return wait;

        Vector3 playerPosition = GameManager.instance.Player.transform.position;
        Vector3 dirVector = transform.position - playerPosition;

        rigidbody.AddForce(dirVector.normalized * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
