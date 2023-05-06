using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public int speed;

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
            bullet.GetComponent<Bullet>().Initalize(damage, -1);
        }
    }
}
