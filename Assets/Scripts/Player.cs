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

    #endregion

    #region [ Properties ]
    public Vector2 InputVecter => inputVecter;
    public Scanner Scanner => scanner;
    public Hand[] Hands => hands;
    #endregion

    #region [ MonoBehaviour Messages ]
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
