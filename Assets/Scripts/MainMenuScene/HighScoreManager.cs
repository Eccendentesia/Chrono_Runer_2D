using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    [Header("UI References")]
    [SerializeField] private TMP_InputField playersName;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private Button submitButton;

    private string player_name;
    private HighScores highScoreScript;
    private int highScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScoreScript = FindFirstObjectByType<HighScores>();

        // Load saved data
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        player_name = PlayerPrefs.GetString("PlayerName", "");

        if (string.IsNullOrEmpty(player_name))
        {
            // No name saved ? show input panel
            inputPanel.SetActive(true);
        }
        else
        {
            // Name already exists ? hide input panel and auto-assign to field
            playersName.text = player_name;
            inputPanel.SetActive(false);

            // Optional: Auto-submit if you want to always keep leaderboard updated
            highScoreScript.SetLeaderBoard(player_name, highScore);
        }

       submitButton.onClick.AddListener(SubmitScore);
    }
   
    public void SubmitScore()
    {
        string inputName = playersName.text;

        if (!string.IsNullOrEmpty(inputName))
        {
            player_name = inputName;

            // Save name for future sessions
            PlayerPrefs.SetString("PlayerName", player_name);
            PlayerPrefs.Save();

            // Submit score to leaderboard
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreScript.SetLeaderBoard(player_name, highScore);
            if (inputPanel != null)
            {
                inputPanel.SetActive(false);
            }
            Settings.Instance.playerName.text = player_name; // Update the player name in Settings UI
        }
    }
}
