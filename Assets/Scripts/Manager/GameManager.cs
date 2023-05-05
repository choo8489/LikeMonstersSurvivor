using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player player;
    [SerializeField] private PoolManager pool;

    public Player Player => player;
    public PoolManager Pool => pool;


    private void Awake()
    {
        instance = this;
    }
}
