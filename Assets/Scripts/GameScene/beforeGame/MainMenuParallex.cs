using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    
    [System.Serializable]
    public class ParallaxLayer
    {
        public Renderer renderer;
        public float scrollSpeed = 0.1f;
        [HideInInspector]
        public Material materialInstance;
    }

    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;

    private void Awake()
    {
        
    }

    private void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        foreach (var layer in layers)
        {
            if (layer.materialInstance != null)
            {
                Vector2 offset = layer.materialInstance.mainTextureOffset;
                offset.x += layer.scrollSpeed * Time.deltaTime;
                layer.materialInstance.mainTextureOffset = offset;
                Debug.Log("Time.deltaTime");
            }
        }
    }
}
