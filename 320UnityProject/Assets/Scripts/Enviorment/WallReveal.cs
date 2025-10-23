using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WallReveal : MonoBehaviour
{
    public Transform player;
    public float fadeDistance = 3f;
    public float fadeSpeed = 3f;
    [Range(0f, 1f)] public float minAlpha = 0.2f;

    private Material mat;
    private Color baseColor;
    private float targetAlpha;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        mat.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
    }

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(player.position, transform.position);
        float t = Mathf.Clamp01(distance / fadeDistance);

        // Player close → more opaque
        targetAlpha = Mathf.Lerp(minAlpha, 1f, t);

        Color current = mat.color;
        current.a = Mathf.Lerp(current.a, targetAlpha, Time.deltaTime * fadeSpeed);
        mat.color = current;
    }
}
