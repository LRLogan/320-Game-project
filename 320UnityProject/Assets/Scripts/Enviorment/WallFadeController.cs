using UnityEngine;

public class WallFadeController : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeStartDistance = 5f;
    public float fadeEndDistance = 2f;
    public float maxAlpha = 1f;

    private Material wallMaterial;
    private Transform player;
    private Color originalColor;

    void Start()
    {
        // Get the material instance
        wallMaterial = GetComponent<Renderer>().material;
        originalColor = wallMaterial.color;

        // Start completely transparent
        Color startColor = originalColor;
        startColor.a = 0f;
        wallMaterial.color = startColor;

        // Enable transparency on the material
        SetupMaterialForTransparency();

        // Find player
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogError("Player not found! Assign player manually or use 'Player' tag.");
    }

    void SetupMaterialForTransparency()
    {
        // Change rendering mode to transparent at runtime
        wallMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        wallMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        wallMaterial.SetInt("_ZWrite", 0);
        wallMaterial.DisableKeyword("_ALPHATEST_ON");
        wallMaterial.EnableKeyword("_ALPHABLEND_ON");
        wallMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // Make sure the material is not opaque
        wallMaterial.SetFloat("_Surface", 1); // 1 = Transparent, 0 = Opaque
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float alpha = CalculateAlpha(distance);

        ApplyAlphaToMaterial(alpha);
    }

    float CalculateAlpha(float distance)
    {
        if (distance >= fadeStartDistance)
            return 0f; // Fully transparent
        else if (distance <= fadeEndDistance)
            return maxAlpha; // Fully opaque (or nearly)
        else
        {
            // Smooth fade between distances
            float normalized = 1f - ((distance - fadeEndDistance) /
                                   (fadeStartDistance - fadeEndDistance));
            return normalized * maxAlpha;
        }
    }

    void ApplyAlphaToMaterial(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = alpha;
        wallMaterial.color = newColor;
    }

    void OnDestroy()
    {
        // Clean up material instance to prevent memory leaks
        if (wallMaterial != null)
            DestroyImmediate(wallMaterial);
    }

    // Optional: Draw gizmos to visualize fade distances in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fadeStartDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fadeEndDistance);
    }
}