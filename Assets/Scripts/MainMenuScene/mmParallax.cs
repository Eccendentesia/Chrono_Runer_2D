using Dan.Enums;
using UnityEngine;

public class mmParallax : MonoBehaviour
{
    public static mmParallax Instance;

    [System.Serializable]
    public class ParallaxLayer
    {
        public Renderer renderer;           // MeshRenderer of the plane
        public float scrollSpeed = 0.1f;    // Speed of the texture scroll
    }
  

    [Header("Tracked Target")]
    public Transform targetTransform; // Assign the player or camera transform here
    public bool isMainMenu = false; 

    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;
    private void Awake()
    {
        moving();
    }
    private void Start()
    {
        moving();
    }
    private void Update()
    {
        transform.position = new Vector3(targetTransform.position.x, 0, -10);
    }

    void LateUpdate()
    {
        moving();
    }
    private void moving()
    {
        if (isMainMenu)
        {
            foreach (var layer in layers)
            {
                Material mat = layer.renderer.material;

                // Scroll the texture offset based on target movement (X-axis only)
                Vector2 offset = mat.mainTextureOffset;
                offset.x += layer.scrollSpeed * Time.deltaTime;
                mat.mainTextureOffset = offset;
            }
        }
    }
}
