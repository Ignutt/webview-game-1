using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Player
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI[] scoresText;
        [SerializeField] private TextMeshProUGUI bestScore;

        private int _score;
        [SerializeField] private UnityEvent onGameOver;
        
        public event UnityAction OnGameOver
        {
            add => onGameOver.AddListener(value);
            remove => onGameOver.RemoveListener(value);
        }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void GameOver()
        {
            onGameOver.Invoke();
        }

        public void IncreaseScore()
        {
            _score += 1;

            foreach (var scoreText in scoresText)
            {
                scoreText.text = _score.ToString();
            }

            bestScore.text = "best " + _score;
        }

        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
