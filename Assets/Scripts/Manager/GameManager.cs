using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float gameTime;
    [SerializeField] private float maxGameTime = 2 * 10f;

    [SerializeField] private Player player;
    [SerializeField] private PoolManager pool;

    public float GameTime => gameTime;
    public Player Player => player;
    public PoolManager Pool => pool;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }
}
