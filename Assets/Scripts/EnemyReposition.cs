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

        Vector3 playerDirection = playerPosition - myPosition;

        switch (transform.tag)
        {
            case "Enemy":
                if (collider.enabled)
                {
                    Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(random + playerDirection * 2);
                }
                break;
        }
    }
}
