using UnityEngine;

public class WallFadeReverseController : MonoBehaviour
{
    [Header("Fade Settings")]
    [Tooltip("Distance at which the wall starts to fade.")]
    public float fadeStartDistance = 5f;

    [Tooltip("Distance at which the wall reaches minimum transparency.")]
    public float fadeEndDistance = 2f;

    [Tooltip("Maximum alpha when far away (fully visible).")]
    [Range(0f, 1f)] public float maxAlpha = 1f;

    [Tooltip("Minimum alpha when very close (still slightly visible).")]
    [Range(0f, 1f)] public float minAlpha = 0.2f;

    private Material wallMaterial;
    private Transform player;
    private Color originalColor;

    void Start()
    {
        // Get material instance
        wallMaterial = GetComponent<Renderer>().material;
        originalColor = wallMaterial.color;

        // Setup transparency
        SetupMaterialForTransparency();

        // Find player
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogError("Player not found! Assign player manually or use 'Player' tag.");
    }

    void SetupMaterialForTransparency()
    {
        wallMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        wallMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        wallMaterial.SetInt("_ZWrite", 0);
        wallMaterial.DisableKeyword("_ALPHATEST_ON");
        wallMaterial.EnableKeyword("_ALPHABLEND_ON");
        wallMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // For URP
        wallMaterial.SetFloat("_Surface", 1);
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
        {
            // Far away → fully visible
            return maxAlpha;
        }
        else if (distance <= fadeEndDistance)
        {
            // Very close → minimum visibility
            return minAlpha;
        }
        else
        {
            // Smooth fade between minAlpha and maxAlpha
            float normalized = (distance - fadeEndDistance) /
                               (fadeStartDistance - fadeEndDistance);
            return Mathf.Lerp(minAlpha, maxAlpha, normalized);
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
        if (wallMaterial != null)
            DestroyImmediate(wallMaterial);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fadeEndDistance); // fully transparent (minAlpha)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fadeStartDistance); // fully visible (maxAlpha)
    }
}
