using UnityEngine;

public class ActiveUIManager : MonoBehaviour
{
    public static ActiveUIManager Instance;
    private void Awake()
    {
         Instance = this;
       
    }
    [SerializeField] private GameObject powerUI;
    [SerializeField] private Transform panelPos;
    private ActiveUI activeui;

    public void activateUI(Upgrades.PowerType type )
    {
        GameObject ui = Instantiate(powerUI, panelPos);
        activeui = ui.GetComponent<ActiveUI>();
        var powers = Upgrades.Instance.powers;

        foreach (var p in powers)
        {
            if (p.powerType == type)
            {
               activeui.setUI(p.upgradeImg, p.duration);
                break;
            }
        }
      
    }
}
