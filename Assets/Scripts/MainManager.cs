using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    private int m_Points;

    public Text HighScoreText;
    private int m_HighScore;
    private const string HighScoreKey = "HighScore";

    public TMP_InputField playerNameInputField;
    private const string PlayerNamekey = "PlayerName";
    private string PlayerName = "_name";

    public GameObject GameOverText;
    public GameObject input_UI;
    private bool m_Started = false;
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // Getting high score
        m_HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        PlayerName = PlayerPrefs.GetString(PlayerNamekey, "_name");
        HighScoreText.text = $"High Score: {PlayerName}: {m_HighScore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            highScore();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    private void highScore()
    {
        if (m_Points > m_HighScore)
        {
            m_HighScore = m_Points;
            PlayerPrefs.SetInt(HighScoreKey, m_HighScore);
            PlayerName = playerNameInputField.text;
            PlayerPrefs.SetString(PlayerNamekey, PlayerName);
            PlayerPrefs.Save();
            HighScoreText.text = $"High Score: {PlayerName}: {m_HighScore}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points>m_HighScore)
        {
            input_UI.SetActive(true); // Show the input field
             playerNameInputField.text = PlayerName; // Set input field text to current player name
        }
        
    }

    public void OkButtonClicked()
    {
        PlayerName = playerNameInputField.text;
        PlayerPrefs.SetString(PlayerNamekey, PlayerName);
        PlayerPrefs.Save();
        input_UI.SetActive(false); // Hide the input field
        HighScoreText.text = $"High Score: {PlayerName}: {m_HighScore}"; // Update high score text with new player name
    }

    public void BackButton()
    {
        if (m_GameOver || !m_Started)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ResetButton()
    {
        m_HighScore = 0;
        PlayerPrefs.SetInt(HighScoreKey, m_HighScore);
        PlayerName = "_name";
        PlayerPrefs.SetString(PlayerNamekey, PlayerName);
        PlayerPrefs.Save();
        HighScoreText.text = $"High Score: {PlayerName}: {m_HighScore}";
    }
}
