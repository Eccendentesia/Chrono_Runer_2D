using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int coinValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadCoins()
    {
        coinValue = PlayerPrefs.GetInt("CoinValue", 0);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("CoinValue", coinValue);
        PlayerPrefs.Save();
    }

    public void increment(int amount)
    {
        coinValue += amount;
        SaveCoins();
        InGameUI.Instance?.UpdateCoinUI(coinValue);
        Upgrades.Instance.coinText.text = coinValue.ToString();
    }

    public void SetCoins(int amount)
    {
        coinValue = amount;
        SaveCoins();
        InGameUI.Instance?.UpdateCoinUI(coinValue);
        Upgrades.Instance.coinText.text = coinValue.ToString();
    }
}
