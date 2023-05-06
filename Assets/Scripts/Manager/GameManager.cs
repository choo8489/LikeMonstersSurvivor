using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("[ Game Control ]")]
    [SerializeField] private float gameTime;
    [SerializeField] private float maxGameTime = 2 * 10f;

    [Header("[ Player Info ]")]
    [Space(10)]
    public int level;
    public int kill;
    public int exp;
    private int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("[ Game Object ]")]
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

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
