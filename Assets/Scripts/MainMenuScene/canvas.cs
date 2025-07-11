using Unity.VisualScripting;
using UnityEngine;

public class canvas : MonoBehaviour
{
    public static canvas instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
}
