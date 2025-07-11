using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [Header("Magnet Effect ")]
    [SerializeField] private float attractRadius = 5f;
    [SerializeField] private float attractSpeed = 10f;
    [SerializeField] private float magnetDuration = 5f;

    public bool isMagnetActive = false;
    private float timer = 0f;

    [SerializeField] private LayerMask coinLayer;

    [Header("Shield Effect")]
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float shieldActiveTime;
    [SerializeField] private float radiusLayerMaskShield;
    [SerializeField] private Collider2D cr;
    [SerializeField] private GameObject shieldEffect;
    private float tempShieldTimeStore;
    public bool isShieldActive;

    [Header("Support powerUp ")]
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject supportBot ;
    [SerializeField] private float shootingDuration;
    [SerializeField] private float shootingInterval;
    private float tempShootingInterval;
    private float tempShootDur;
    public bool isShootingActive;

    [Header("Score Doubler ")]
    [SerializeField] public int initailValue;
    [SerializeField] public int doubleScore;
    [SerializeField] public bool isScoreDoubleActive;
    [SerializeField] private float scoreDuration;
    private float tempDur;

    [Header("Script Reference")]
    private PlayerMove player;
    private InGameUI gameUI;
    private PlayerFollower follower;
    [Header("Collect Audio")]
    [SerializeField] private AudioSource collectAudio;

    private void Start()
    {
       
        player = FindFirstObjectByType<PlayerMove>();
        gameUI = FindFirstObjectByType<InGameUI>();
        follower = FindFirstObjectByType<PlayerFollower>();
        cr = GetComponent<Collider2D>();

        tempDur = scoreDuration;
        tempShootDur = shootingDuration;
        tempShootingInterval = shootingInterval;
        tempShieldTimeStore = shieldActiveTime;

        upgradeSetting();
    }

    private void Update()
    {
        if (isShieldActive) shieldEffect.SetActive(true);
        else shieldEffect.SetActive(false);

        doubleTheSCore();
        powerUpTimers();
      
    }

    private void upgradeSetting()
    {
        var powers = Upgrades.Instance.powers;

        foreach (var p in powers)
        {
            switch (p.powerType)
            {
                case Upgrades.PowerType.magnet:
                    magnetDuration = p.duration;
                    break;
                case Upgrades.PowerType.shield:
                    shieldActiveTime = p.duration;
                    tempShieldTimeStore = p.duration;
                    break;
                case Upgrades.PowerType.support:
                    shootingDuration = p.duration;
                    tempShootDur = p.duration;
                    break;
                case Upgrades.PowerType.score:
                    scoreDuration = p.duration;
                    break;
            }
        }
    }

    private void powerUpTimers()
    {
        if (isMagnetActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                isMagnetActive = false;
            }
            else
            {
                AttractCoins();
            }
        }

        if (isShieldActive)
        {
            ActivateShield();
            shieldActiveTime -= Time.deltaTime;
            if (shieldActiveTime <= 0)
            {
                isShieldActive = false;
                shieldActiveTime = tempShieldTimeStore;
            }
        }
        else DeactivateShield();

        if (isShootingActive && player.receiveInput)
        {
           
            shootingDuration -= Time.deltaTime;
            if (shootingDuration <= 0)
            {
                isShootingActive = false;
                shootingDuration = tempShootDur;
            }
        }
        if (!isShootingActive)
        {
            shootingInterval = tempShootingInterval;
        }

        if (isScoreDoubleActive)
        {
            scoreDuration -= Time.deltaTime;
            if (scoreDuration <= 0)
            {
                isScoreDoubleActive = false;
                scoreDuration = tempDur;
            }
        }
    }

    private void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, attractRadius, coinLayer);

        foreach (Collider2D coin in coins)
        {
            Transform coinTransform = coin.transform;
            coinTransform.position = Vector2.MoveTowards(coinTransform.position, transform.position, attractSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet"))
        {
            if (Settings.Instance.isPlayingSound) collectAudio.PlayOneShot(collectAudio.clip);
            ActivateMagnet();
            ActiveUIManager.Instance.activateUI(Upgrades.PowerType.magnet);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            if (Settings.Instance.isPlayingSound) collectAudio.PlayOneShot(collectAudio.clip);
            isShieldActive = true;
            ActiveUIManager.Instance.activateUI(Upgrades.PowerType.shield);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Support"))
        {
            if (Settings.Instance.isPlayingSound) collectAudio.PlayOneShot(collectAudio.clip);
            isShootingActive = true;
            shooting();
            ActiveUIManager.Instance.activateUI(Upgrades.PowerType.support);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ScoreDoubler"))
        {
            if (Settings.Instance.isPlayingSound) collectAudio.PlayOneShot(collectAudio.clip);
            isScoreDoubleActive = true;
            ActiveUIManager.Instance.activateUI(Upgrades.PowerType.score);
            Destroy(collision.gameObject);
        }
    }

 


    public void ActivateMagnet()
    {
        isMagnetActive = true;
        timer = magnetDuration;
    }

    private void ActivateShield()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusLayerMaskShield, enemy);
        foreach (var collider in colliders)
        {
            Physics2D.IgnoreCollision(cr, collider, true);
        }
    }

    private void DeactivateShield()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusLayerMaskShield, enemy);
        foreach (var collider in colliders)
        {
            Physics2D.IgnoreCollision(cr, collider, false);
        }
    }

    private void shooting()
    {
      GameObject bot = Instantiate(supportBot, SpawnPoint.position, Quaternion.identity);
        Destroy(bot , shootingDuration);
    }

    private void doubleTheSCore()
    {
        if (player.receiveInput && follower.followPlayer )
        {
            gameUI.scoreTimer += Time.deltaTime;
            if (gameUI.scoreTimer >= 1f)
            {
                if (isScoreDoubleActive) gameUI.AddScore(doubleScore);
                else gameUI.AddScore(initailValue);

                gameUI.scoreTimer = 0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractRadius);
        Gizmos.DrawWireSphere(transform.position, radiusLayerMaskShield);
    }
}
