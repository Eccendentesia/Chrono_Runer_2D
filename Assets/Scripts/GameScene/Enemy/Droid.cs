using UnityEngine;
using System.Collections;

public class Droid : MonoBehaviour
{
    [SerializeField] private float attackTime;
    [SerializeField] private GameObject bullet ;
    [SerializeField] private Transform bulletSpawnPoint ;
    [SerializeField] private Animator anim;
    private float attackAnimationTime;
    private float tempTimer;
    void Start()
    {
       anim = GetComponent<Animator>();
        tempTimer = attackTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        Attackanimation();
    }
    private void Attackanimation()
    {
        attackTime -= Time.deltaTime;
        if(attackTime <= 0)
        {
            anim.SetTrigger("Attack");
            Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
            attackTime = tempTimer;
        }
       
    }
  
       
    
}
