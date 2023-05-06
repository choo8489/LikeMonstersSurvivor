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
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
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

    public void Initialize()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Arrange();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Arrange();
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
        //bullet.GetComponent<Bullet>().Initalize(damage, count, dir);
        bullet.GetComponent<Bullet>().Initalize(damage, count, dir);
    }
}
