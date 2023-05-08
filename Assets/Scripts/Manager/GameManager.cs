using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("[ Game Control ]")]
    public bool isLive;
    [SerializeField] private float gameTime;
    [SerializeField] private float maxGameTime = 2 * 10f;

    [Header("[ Player Info ]")]
    [Space(10)]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    private int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("[ Game Object ]")]
    [SerializeField] private Player player;
    [SerializeField] private PoolManager pool;
    public LevelUp uiLevelUp;

    public float MaxGameTime => maxGameTime;
    public float GameTime => gameTime;
    public Player Player => player;
    public PoolManager Pool => pool;
    public int[] NextExp => nextExp;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop() 
    {
        isLive = false;
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
