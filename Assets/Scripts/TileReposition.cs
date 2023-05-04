using UnityEngine;

public class TileReposition : MonoBehaviour
{
    private readonly string Area = "Area";

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
            case "Ground":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * directionX * 40);
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * directionY * 40);
                break;
            case "Enemy":
                break;
        }
    }
}
