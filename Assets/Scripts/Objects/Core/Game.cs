using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    private int _score;

    public int Score => _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
    }
}