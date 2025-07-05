using UnityEngine;
using UnityEngine.UI;

public class game_controller_NQAnh : MonoBehaviour
{
    public static game_controller_NQAnh instance;
    public Text scoreText;               
    public Text gameOverScoreText;       
    public GameObject gameOverText;    
    public bool isGameOver = false;      
    private int score = 0;                

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }

    public void GameOver()
    {
        gameOverScoreText.text = "Your Score: " + this.score;
        gameOverText.SetActive(true);
        isGameOver = true;
    }

    public void CollectCoin(int coinValue)
    {
        AddScore(coinValue); 
    }

    void Start()
    {
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
    }

}
