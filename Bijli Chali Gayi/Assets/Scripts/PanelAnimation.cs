using UnityEngine;
using UnityEngine.UI;

public class CanvasBackgroundAnimator : MonoBehaviour
{
    public enum AnimationType
    {
        ColorGradient,
        PulseGlow,
        TextureScroll,
        SlowRotate,
        WaveLight
    }

    [Header("🎯 Target Panel")]
    public Image targetPanel; // Drag your Panel here

    [Header("🎞 Choose Animation Style")]
    public AnimationType animationType = AnimationType.ColorGradient;

    [Header("⚙️ Common Settings")]
    public float speed = 1f;

    [Header("🎨 Gradient Colors (for ColorGradient)")]
    public Color colorA = new Color(0.1f, 0.2f, 0.3f);
    public Color colorB = new Color(0.0f, 0.6f, 1.0f);

    [Header("💫 Pulse Settings (for PulseGlow)")]
    public float glowStrength = 0.3f;

    [Header("🌀 Scroll Settings (for TextureScroll)")]
    public Vector2 scrollSpeed = new Vector2(0.05f, 0.05f);

    [Header("🌊 WaveLight Settings (for WaveLight)")]
    public Color waveColor = new Color(0.5f, 0.8f, 1f);
    public float waveFrequency = 2f;
    public float waveAmplitude = 0.5f;

    private Material mat;
    private Color baseColor;
    private float t;

    void Start()
    {
        if (targetPanel == null)
        {
            Debug.LogWarning("⚠️ Please assign a Panel Image in the inspector!");
            return;
        }

        baseColor = targetPanel.color;

        if (targetPanel.material != null)
        {
            mat = Instantiate(targetPanel.material);
            targetPanel.material = mat;
        }
    }

    void Update()
    {
        if (targetPanel == null) return;

        switch (animationType)
        {
            case AnimationType.ColorGradient:
                AnimateColorGradient();
                break;
            case AnimationType.PulseGlow:
                AnimatePulseGlow();
                break;
            case AnimationType.TextureScroll:
                AnimateTextureScroll();
                break;
            case AnimationType.SlowRotate:
                AnimateSlowRotate();
                break;
            case AnimationType.WaveLight:
                AnimateWaveLight();
                break;
        }
    }

    // 🌈 Smooth Color Gradient
    void AnimateColorGradient()
    {
        t += Time.deltaTime * speed;
        float lerp = (Mathf.Sin(t) + 1f) / 2f;
        targetPanel.color = Color.Lerp(colorA, colorB, lerp);
    }

    // ✨ Pulse Glow
    void AnimatePulseGlow()
    {
        float glow = (Mathf.Sin(Time.time * speed) + 1f) / 2f * glowStrength;
        targetPanel.color = baseColor + new Color(glow, glow, glow, 0);
    }

    // 🔁 Moving Texture
    void AnimateTextureScroll()
    {
        if (mat == null) return;
        Vector2 offset = mat.mainTextureOffset;
        offset += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }

    // 🌀 Slow Background Rotation (for fancy feel)
    void AnimateSlowRotate()
    {
        targetPanel.transform.Rotate(Vector3.forward, speed * Time.deltaTime * 10f);
    }

    // 🌊 Wave-like light shifting
    void AnimateWaveLight()
    {
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        targetPanel.color = Color.Lerp(baseColor, waveColor, (wave + 1f) / 2f);
    }
}
