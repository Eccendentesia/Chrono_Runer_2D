using UnityEngine;
using CrazyGames;
using JetBrains.Annotations;

public class CrazyAdManager : MonoBehaviour
{
    public static CrazyAdManager Instance;

    private AdModule adModule;

  
    private void Awake()
    {
        Instance = this;

        // Initialize CrazySDK (important)
        CrazySDK.Init(() =>
        {
            Debug.Log("CrazySDK Initialized Successfully!");
            adModule = FindFirstObjectByType<AdModule>();
        });
        Instance = this;
        adModule = FindFirstObjectByType<AdModule>();  // Ensure AdModule is in the sc
    }
    public void ShowRewardedAdToRevive()
    {
        if (adModule == null)
        {
            Debug.LogError("AdModule not found!");
            return;
        }

        adModule.RequestAd(
            CrazyAdType.Rewarded,
            () => { Debug.Log("Ad Started"); },                // Optional
            (error) => { Debug.Log("Ad Error: " + error); },  // Skipped or error
            () => {
                Debug.Log("Ad Finished—Revive Player!");
                 // Call revive logic
            }
        );
       
    }
   
}
