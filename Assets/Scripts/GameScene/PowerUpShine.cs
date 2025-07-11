using UnityEngine;
using System.Collections;

public class PowerUpShine : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isRotating = true;
    private bool isBursting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }
    private void Spin()
    {
        if(isRotating)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime );
        }
    }
  

}
