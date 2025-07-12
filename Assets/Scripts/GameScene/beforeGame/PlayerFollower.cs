using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject _mech;
   
    public float waitTime;
    [SerializeField] private float smoothnessWhenMovingToMech = 2f;

    [Header("Camera Shake Settings")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    [Header("Script Reference")]
    private PlayerMove playerScript;
    private EnemyChaser enemy;

    public bool followPlayer = false;
    public Vector3 offset = new Vector3(30f, 0, 0);    

    private void Start()
    {
        enemy = FindFirstObjectByType<EnemyChaser>();
        playerScript = FindFirstObjectByType<PlayerMove>();
      
    }

    void Update()
    {
        if (followPlayer && playerScript != null && playerScript.receiveInput)
        {
           
            transform.position = new Vector3(Player.transform.position.x  + offset.x , transform.position.y, -10f);
        }
        else transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

 
    // ?? Public method to trigger camera shake
    public void TriggerCameraShake()
    {
        StopAllCoroutines();  // Stop existing shakes to prevent stacking
        StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(transform.position.x + x, transform.position.y, -10);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }
}
