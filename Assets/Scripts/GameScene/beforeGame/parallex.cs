using UnityEngine;

public class MaterialParallaxScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Renderer renderer;
        public float baseScrollSpeed = 0.1f;
        [HideInInspector] public float currentScrollSpeed;
    }

    private PlayerMove player;
    private PlayerFollower follower;

    [Header("Tracked Target")]
    public Transform targetTransform;

    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;

    private int nextScoreThreshold = 500;      // First speed-up happens at 500
    public float speedMultiplier = 0.09f;       // Speed increase factor
    public int scoreStep = 600;                // Every X points

    void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        follower = FindFirstObjectByType<PlayerFollower>();

        foreach (var layer in layers)
        {
            layer.currentScrollSpeed = layer.baseScrollSpeed;
        }
    }

    private void Update()
    {
        if (targetTransform != null)
            transform.position = new Vector3(targetTransform.position.x, 0, -10);

        AdjustParallaxSpeed();
    }

    void LateUpdate()
    {
        if (player != null && player.receiveInput && follower.followPlayer)
        {
            foreach (var layer in layers)
            {
                Material mat = layer.renderer.material;
                Vector2 offset = mat.mainTextureOffset;
                offset.x += layer.currentScrollSpeed * Time.deltaTime;
                mat.mainTextureOffset = offset;
            }
        }
    }

    private void AdjustParallaxSpeed()
    {
        int score = InGameUI.Instance.score;

        // Check if score has crossed the current threshold
        if (score >= nextScoreThreshold)
        {
            MultiplySpeed(speedMultiplier);

            // Set the next threshold (e.g., 1000, 1500, 2000, etc.)
            nextScoreThreshold += scoreStep;
        }
    }

    private void MultiplySpeed(float factor)
    {
        foreach (var layer in layers)
        {
            layer.currentScrollSpeed += factor;
        }
    }
}
