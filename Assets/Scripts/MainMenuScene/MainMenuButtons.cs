using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour
{
    public static MainMenuButtons Instance;

    [SerializeField] public GameObject MainMenu;
    [SerializeField] private GameObject UpgradesPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject LeaderBoardPanel;
    [SerializeField] private Button PlayButton;


   
    void Start()
    {
        MainMenu.SetActive(true);
        UpgradesPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        LeaderBoardPanel.SetActive(false);
        PlayButton.onClick.AddListener(loadGameScene);
       
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(gameObject); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnUpgradesButtonClicked()
    {
        UpgradesPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    
    public void OnSettingsButton()
    {
        SettingsPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void OnLeaderBoardButtonClicked()
    {
        LeaderBoardPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    //Deactivate Panels 
  
    public void OnUpgradesCancelButtonClicked()
    {
        UpgradesPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
  
    public void OnSettingsCancelButton()
    {
        SettingsPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void OnLeaderBoardCancelButtonClicked()
    {
        LeaderBoardPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
    private void loadGameScene()
    {
       
      
        MainMenu.SetActive(false);
        LoadingScene.Instance.LoadSceneByName("GameScene"); 

    }

}
