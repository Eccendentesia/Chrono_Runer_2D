using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class SwitchPlatform : MonoBehaviour
{
    private CompositeCollider2D currentPlatform;
    [SerializeField] private float switchTime; 
    [SerializeField] private BoxCollider2D boxCollider;

    private bool canDrop = false;
    private PlayerMove player;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
        if (canDrop && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && player.receiveInput )
        {
            StartCoroutine(TemporarilyMakePlayerTrigger());
        }
    }
    public void downAction()
    {
        if (canDrop && player.receiveInput)
        {
            StartCoroutine(TemporarilyMakePlayerTrigger());
        }
    }
    private  IEnumerator TemporarilyMakePlayerTrigger()
    {
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(switchTime); // Duration during which player can fall
        boxCollider.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpperGround") || collision.gameObject.CompareTag("MidGround"))
        {
            canDrop = true;
            currentPlatform = collision.gameObject.GetComponent<CompositeCollider2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CompositeCollider2D>() == currentPlatform)
        {
            canDrop = false;
            currentPlatform = null;
        }
    }
}
