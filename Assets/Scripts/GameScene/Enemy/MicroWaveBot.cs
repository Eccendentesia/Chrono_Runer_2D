using UnityEngine;

public class MicroWaveBot: MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private string botname;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D cr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedAdder = 2f ;
    private int speedThreshold = 200;
   
    void Start()
    {
     
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
       if(InGameUI.Instance.score >= speedThreshold)
        {
            speed += speedAdder;
            speedThreshold += 500; // Increase threshold for next speed increase
        }

       

    }
}
