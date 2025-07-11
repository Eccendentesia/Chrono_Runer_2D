using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Runtime.CompilerServices;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [SerializeField] private GameObject ResumePanel;
    [SerializeField] private GameObject LostPanel;

    [Header("UI Manager")]
    [SerializeField] public TextMeshProUGUI CoinCounter;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI AfterLoseHighScoreText;
    [SerializeField] private TextMeshProUGUI AfterLoseScoreText;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button JumpButton;
    [SerializeField] private Button DownButton;

   

    [Header("Counters Data in int")]
    public int score;
    private int highScore;
    public float lostPanelDisplayTimer;

    [Header("Audio")]
    [SerializeField] private AudioSource Music;

    [Header("Script References")]
    [SerializeField] private PlayerMove player;
    private PowerUp power;
    private SwitchPlatform platform;
    private bool hasShownLostPanel;
    public bool playerCollidedWithEnemy;

    public float scoreTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        QuitButton.onClick.AddListener(loadMainMenu);
        player = FindFirstObjectByType<PlayerMove>();
        platform = FindFirstObjectByType<SwitchPlatform>();
        power = FindFirstObjectByType<PowerUp>();
        Music = GetComponent<AudioSource>();

        Time.timeScale = 1f;
        playerCollidedWithEnemy = false;
        LostPanel.SetActive(false);
        ResumePanel.SetActive(false);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
      

        UpdateCoinUI(CoinManager.Instance.coinValue);
        UpdateScoreUI();

        JumpButton.onClick.AddListener(player.JumpOnce);
        DownButton.onClick.AddListener(platform.downAction);

        if (Settings.Instance.isPlayingMusic)
            Music.Play();
       UpdateCoinUI(CoinManager.Instance.coinValue);
        UpdateScoreUI();
    }

    private void Update()
    {
        lostPanelActivation();

    }

    public void AddScore(int value)
    {
        score += value;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

 

    public void UpdateCoinUI(int value)
    {
        CoinCounter.text = "Coins : " + value;
       
    }


    private void UpdateScoreUI()
    {
        ScoreText.text = "Score : " + score;
        HighScoreText.text = "HighScore : " + highScore;
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0f;
        ResumePanel.SetActive(true);
    }

    public void OnResumeButtonClicked()
    {
        ResumePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        ActiveUIManager.Instance.activateUI(Upgrades.PowerType.shield);
        player.speed = player.tempSpeed;
        power.isShieldActive = true;
        player.receiveInput = true;
        hasShownLostPanel = false;
        playerCollidedWithEnemy = false;
        LostPanel.SetActive(false);
    }

    private void AfterLostScoreUI()
    {
        AfterLoseHighScoreText.text = "HighScore :" + highScore;
        AfterLoseScoreText.text = "Score :" + score;
    }

    private void lostPanelActivation()
    {
        if (playerCollidedWithEnemy && !hasShownLostPanel)
        {
            hasShownLostPanel = true;
            StartCoroutine(lostPanelDisplayTime());
        }
    }

    private IEnumerator lostPanelDisplayTime()
    {
        AfterLostScoreUI();
        SaveHighScore();
        yield return new WaitForSeconds(lostPanelDisplayTimer);
        LostPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void SaveHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void loadMainMenu()
    {

        Time.timeScale = 1f;
        MainMenuButtons.Instance.MainMenu.SetActive(true);
        SceneManager.LoadScene("MainMenu");
        HighScoreManager.Instance.SubmitScore();
    }
  
}