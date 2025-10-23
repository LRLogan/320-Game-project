using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WallRevealPartial : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Fade Settings")]
    [Tooltip("How close the player needs to be for the wall to reach full opacity.")]
    public float fadeDistance = 3f;

    [Tooltip("The minimum opacity when the player is far away.")]
    [Range(0f, 1f)] public float minAlpha = 0.2f;

    [Tooltip("How quickly the wall transitions between opacity states.")]
    public float fadeSpeed = 3f;

    [Tooltip("A curve controlling the fade behavior over distance.")]
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Vertical Fade Settings")]
    [Tooltip("How high up the wall the fade effect starts (world Y position offset).")]
    public float fadeStartHeight = 0.5f;

    [Tooltip("How tall the fade region is from the fade start height upward.")]
    public float fadeHeight = 2f;

    private Material mat;
    private Color originalColor;
    private float targetAlpha;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;

        // Start more transparent
        Color c = mat.color;
        c.a = minAlpha;
        mat.color = c;
    }

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // Normalize distance (0 close, 1 far)
        float t = Mathf.Clamp01(distance / fadeDistance);
        float curvedT = fadeCurve.Evaluate(1f - t);

        // Compute target alpha based on distance
        targetAlpha = Mathf.Lerp(minAlpha, 1f, curvedT);

        // Apply gradient fade based on height
        ApplyVerticalFade();
    }

    void ApplyVerticalFade()
    {
        // Smooth transition per frame
        Color[] colors = new Color[4];
        Color baseColor = mat.color;

        // Simulate vertical fade by using a material property block (if shader supports it)
        // For simplicity, this sets a single alpha averaged for top/bottom — for full gradient, use shader approach
        Color newColor = baseColor;
        float playerY = player.position.y;

        // If player is below fade start height, make top more visible
        float heightT = Mathf.InverseLerp(fadeStartHeight, fadeStartHeight + fadeHeight, playerY);

        // The higher the player (closer to top), the more opaque
        float finalAlpha = Mathf.Lerp(minAlpha, targetAlpha, heightT);
        newColor.a = Mathf.Lerp(baseColor.a, finalAlpha, Time.deltaTime * fadeSpeed);

        mat.color = newColor;
    }
}
