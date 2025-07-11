using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    public static Settings Instance;

    [Header("Buttons")]
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button ResetButton;

    [Header("Text UI")]
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] public TextMeshProUGUI playerName; 

    [Header("Booleans")]
    public bool isPlayingSound;
    public bool isPlayingMusic;
    public bool isControlActive;

    [Header("Audio ")]
    [SerializeField] public AudioSource  music;

    [Header("Script Reference")]
    private  Upgrades upgrade;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(gameObject); }
        isPlayingMusic = PlayerPrefs.GetInt("Music", 1) == 1;
    }
    private void Start()
    {
        playerName.text = PlayerPrefs.GetString("PlayerName");
        music = GetComponent<AudioSource>();


        // Load saved settings (default to true)
        isPlayingSound = PlayerPrefs.GetInt("Sound", 1) == 1;
       
        isControlActive = PlayerPrefs.GetInt("Control", 1) == 1;
        SceneManager.sceneLoaded += OnSceneLoaded;

        UpdateUI();

        // Add listeners
        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);
        ResetButton.onClick.AddListener(ResetSettings);
    
    //Scripts 
    upgrade = FindFirstObjectByType<Upgrades>();

    }
    private void Update()
    {
    }

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "MainMenu")
    {
        if (isPlayingMusic && !music.isPlaying)
            music.Play();
    }
    else
    {
        if (music.isPlaying)
            music.Stop();
    }
}

      
    
    private void ToggleSound()
    {
        isPlayingSound = !isPlayingSound;
        PlayerPrefs.SetInt("Sound", isPlayingSound ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
    }

    private void ToggleMusic()
    {
        isPlayingMusic = !isPlayingMusic;
        if (isPlayingMusic) music.Play();
        else music.Stop();
        PlayerPrefs.SetInt("Music", isPlayingMusic ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
    }

  

    private void UpdateUI()
    {
        soundText.text = "Sound: " + (isPlayingSound ? "ON" : "OFF");
        musicText.text = "Music: " + (isPlayingMusic ? "ON" : "OFF");
      
    }
    private void ResetSettings()
    {
        // 1. Preserve Player Name (assuming key is "PlayerName")
        string savedPlayerName = PlayerPrefs.GetString("PlayerName", "");

        // 2. Clear everything else
        PlayerPrefs.DeleteAll();

        // 3. Restore Player Name
        PlayerPrefs.SetString("PlayerName", savedPlayerName);

        // 4. Reset Coins specifically
        PlayerPrefs.SetInt("CoinValue", 0);

        // 5. Reset Powers safely
        if (upgrade != null && upgrade.powers != null)
        {
            foreach (var power in upgrade.powers)
            {
                power.price = 100;
                power.duration = 7;
                power.SaveData();
                power.UpdateUI();
            }
        }

        // 6. Save changes
        PlayerPrefs.Save();

        // 7. Update UI
        UpdateUI();

        CoinManager.Instance.SetCoins(0);
        Debug.Log("Settings reset. Player Name preserved: " + savedPlayerName);
    }



}
