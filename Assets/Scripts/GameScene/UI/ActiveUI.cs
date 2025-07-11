using UnityEngine;
using UnityEngine.UI;

public class ActiveUI : MonoBehaviour
{
    public Slider activeDuration;
    public  Image activeImage ;

    private float timer;
    private float duration;

    public void setUI(Sprite s , float duration)
    {
        activeImage.sprite  = s;
        activeDuration.maxValue = duration;
        activeDuration.value = duration;
        timer = duration;
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        activeDuration.value = timer;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
