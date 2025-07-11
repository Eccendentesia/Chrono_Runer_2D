using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject _mech;
    private Vector3 originalScale;
    public float waitTime;
    [SerializeField] private float smoothnessWhenMovingToMech = 2f;

    [Header("Camera Shake Settings")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    [Header("Script Reference")]
    private PlayerMove playerScript;
    private EnemyChaser enemy;

    public bool followPlayer = false;
    private Vector3 initialCamPos;

    private void Start()
    {
        enemy = FindFirstObjectByType<EnemyChaser>();
        playerScript = FindFirstObjectByType<PlayerMove>();
       // StartCoroutine(MoveFromPlayerToMech());
        originalScale = playerScript.transform.localScale;
       // initialCamPos = transform.position;
    }

    void Update()
    {
        if (followPlayer && playerScript != null && playerScript.receiveInput)
        {
            /*  Vector3 targetPos = new Vector3(Player.transform.position.x, transform.position.y, -10f);
              transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
              Player.transform.localScale = originalScale;
              initialCamPos = transform.position;  // Keep updating base position while following*/
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, -10f);
        }
        else transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

 /*   private IEnumerator MoveFromPlayerToMech()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, -10f);
        yield return new WaitForSeconds(waitTime);

        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(_mech.transform.position.x, transform.position.y, -10f);

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * smoothnessWhenMovingToMech;
            transform.position = Vector3.Lerp(startPos, endPos, elapsed);
            yield return null;
        }

        enemy.enemiesMsg();
        yield return new WaitForSeconds(2f);
        playerScript.receiveInput = true;
        playerScript.speed = playerScript.tempSpeed;
        followPlayer = true;
    }
 */
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
