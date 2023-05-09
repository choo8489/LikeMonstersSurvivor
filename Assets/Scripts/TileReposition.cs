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



        switch (transform.tag)
        {
            case "Ground":
                float diffX = (playerPosition.x - myPosition.x);
                float diffY = (playerPosition.y - myPosition.y);

                float directionX = diffX < 0 ? -1 : 1;
                float directionY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

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
