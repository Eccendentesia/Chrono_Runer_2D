using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public static Upgrades Instance;
    public enum PowerType { magnet, shield, support, score };

    [SerializeField] public TextMeshProUGUI coinText;

    [System.Serializable]
    public class Power
    {
        [Header("Upgrade Data")]
        public string upgradeName;
        public float duration = 7f;
        public float durationAdder = 1.5f;
        public int price;
        public int priceMultiplier = 2;
        public Sprite upgradeImg;
        public float maxDuration = 20f;
        public PowerType powerType;

        [Header("Upgrade UI")]
        public TextMeshProUGUI UName;
        public TextMeshProUGUI UPrice;
        public TextMeshProUGUI UDuration;
        public Image UImage;
        public Button UpgradeButton;
        public Slider DurationBar;

        public void Init()
        {
            duration = PlayerPrefs.GetFloat(upgradeName + "_Duration", duration);
            price = PlayerPrefs.GetInt(upgradeName + "_Price", price);
            UpgradeButton.onClick.AddListener(Upgrade);
            UpdateUI();
        }

        public void Upgrade()
        {

            if ( CoinManager.Instance.coinValue >= price)
            {
                if (duration >= maxDuration) return;

                CoinManager.Instance.SetCoins(CoinManager.Instance.coinValue - price);
                duration += durationAdder;
                if (duration > maxDuration) duration = maxDuration;

                price *= priceMultiplier;
                SaveData();
                UpdateUI();
               
                   
                
            }
        }

        public void SaveData()
        {
            PlayerPrefs.SetFloat(upgradeName + "_Duration", duration);
            PlayerPrefs.SetInt(upgradeName + "_Price", price);
            PlayerPrefs.Save();
        }

        public void UpdateUI()
        {
            DurationBar.maxValue = maxDuration;
            DurationBar.value = duration;

            UName.text = upgradeName;

            UPrice.text = (duration >= maxDuration) ? "MAXED" : price.ToString();
            UDuration.text = "Duration: " + duration.ToString("0.0") + "s";

           

            UpgradeButton.interactable = duration < maxDuration;
        }
    }

    [Header("All Powers")]
    public Power[] powers;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (var power in powers)
        {
            power.Init();
        }

        int savedCoins = PlayerPrefs.GetInt("CoinValue", 0);
        coinText.text = "" + savedCoins; 
    }

}
