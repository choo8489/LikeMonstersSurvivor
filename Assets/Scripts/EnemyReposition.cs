using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyReposition : MonoBehaviour
{
    private readonly string Area = "Area";

    private Collider2D collider;

    private void Awake()
    {
        TryGetComponent(out collider);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Area))
            return;

        Vector3 playerPosition = GameManager.instance.Player.transform.position;
        Vector3 myPosition = transform.position;

        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        Vector3 playerDirection = GameManager.instance.Player.InputVecter;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Enemy":
                if (collider.enabled)
                {
                    // 플레이어의 이동 방향에 따라 맞은 편에서 등장하도록 이동
                    transform.Translate(playerDirection * 20 
                        + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
