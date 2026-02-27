using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private int _score;

    public int Score => _score;

    private void Start()
    {
        _enemySpawner.Initialize(this, _bulletSpawner);
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _bird.Died += OnBirdDied;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _bird.Died -= OnBirdDied;
    }

    private void OnBirdDied()
    {
        Time.timeScale = 0f;
        _enemySpawner.StopSpawning();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bird.Reset();
        _enemySpawner.ResetPool();
        _bulletSpawner.ResetPool();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        _bird.GetComponent<ScoreCounter>()?.Add();
    }
}