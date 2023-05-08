using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;
    private Player player;

    private void Awake()
    {
        player = GameManager.instance.Player;
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
    }

    public void Initialize(ItemData data)
    {
        // Basic Set
        name = $"Weapon {data.itemId}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int i = 0; i < GameManager.instance.Pool.Prefabs.Length; i++)
        {
            if (data.projectile == GameManager.instance.Pool.Prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Arrange();
                break;
            default:
                speed = 0.3f * Character.WeaponRate;
                break;
        }

        //Hand Set
        Hand hand = player.Hands[(int)data.itemType];
        hand.spriteRenderer.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
            Arrange();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private void Arrange()
    {
        for (int i = 0; i < count; i++) 
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.Pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            bullet.Rotate(Vector3.forward * 360 * i / count);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // -1 is Infinity Per;
            bullet.GetComponent<Bullet>().Initalize(damage, -1, Vector3.zero);
        }
    }

    private void Fire()
    {
        if (!player.Scanner.nearestTarget)
            return;

        Vector3 targetPosition = player.Scanner.nearestTarget.position;
        Vector3 dir = (targetPosition - transform.position).normalized;

        Transform bullet = GameManager.instance.Pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Initalize(damage, count, dir);
    }
}
