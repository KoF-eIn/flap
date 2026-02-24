using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private void Update()
    {
        if (Game.Instance != null)
            _scoreText.text = "Score: " + Game.Instance.Score;
    }
}