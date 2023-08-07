using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    #region [ Variables ]
    private readonly string SPEED = "Speed";

    [SerializeField] private float speed;

    [SerializeField] private Vector2 inputVecter;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private Scanner scanner;
    private Hand[] hands;

    public RuntimeAnimatorController[] animationController;

    #endregion

    #region [ Properties ]
    public Vector2 InputVecter => inputVecter;
    public Scanner Scanner => scanner;
    public Hand[] Hands => hands;
    #endregion

    #region [ MonoBehaviour Messages ]
    private void OnEnable()
    {
        Material material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_SplitValue", - 1.0f);
        material.DOFloat(1.0f, "_SplitValue", 1.0f).OnComplete(() =>
        {
            speed *= Character.Speed;
            animator.runtimeAnimatorController = animationController[GameManager.instance.playerId];
        });
    }

    private void Awake()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        TryGetComponent(out scanner);

        hands = GetComponentsInChildren<Hand>(true);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        rigidbody.MovePosition(rigidbody.position + (inputVecter * speed * Time.fixedDeltaTime));
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        animator.SetFloat(SPEED, inputVecter.magnitude);

        if (inputVecter.x != 0)
        {
            spriteRenderer.flipX = inputVecter.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10f;

        if (GameManager.instance.health < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

    #endregion

    #region [ InputSystem Message ]
    private void OnMove(InputValue value)
    {
        inputVecter = value.Get<Vector2>();
    }
    #endregion

    #region [ Public Methods ]

    public void SetSpped(float speed) => this.speed = speed;

    #endregion

    #region [ Private Methods ]

    #endregion
}
