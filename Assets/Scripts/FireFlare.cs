using System.Collections;
using UnityEngine;

public class FireFlare : MonoBehaviour {

    [System.Serializable]
    
    public class FlameLayer {
        public SpriteRenderer sprite;
        [Range(0, 1)] public float startHeat = 0f; // heat at which this layer starts to appear
        public float minScale = 0f; // size when it first appears (x normal size)
        public float maxScale = 1.2f; // size at full heat
        [Range(0, 1)] public float minAlpha = 0f;
        [Range(0, 1)] public float maxAlpha = 1f;
    }

    [Header("Flame layers, back to front (rc, oc, yc, wc)")]
    public FlameLayer[] layers;

    [Header("Timing")]
    public float stagger = 0.04f; // delay between each layer flaring (seconds)
    public float bumpPerClick = 0.35f; // heat one click adds (0 to 1)
    public float decayPerSecond = 0.8f; // how fast heat drains between clicks
    public float smoothing = 15f; // higher = snappier, lower = softer

    float[] heat; // target heat for each layer
    float[] shown; // smoothed heat that's actually displayed
    Vector3[] baseScale; // each layer's normal size, from the scene

    void Awake() {
        heat = new float[layers.Length];
        shown = new float[layers.Length];
        baseScale = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++) {
            baseScale[i] = layers[i].sprite.transform.localScale;
        }
    }

    void OnEnable() {Clickable.OnClick += HandleClick;}
    void OnDisable() {Clickable.OnClick -= HandleClick;}

    void HandleClick(int totalClicks) {
        // Each layer flares a little after the previous one
        for (int i = 0; i < layers.Length; i++) {
            StartCoroutine(BumpLayer(i, i * stagger));
        }
    }

    IEnumerator BumpLayer(int i, float delay) {
        yield return new WaitForSeconds(delay);
        heat[i] = Mathf.Min(1f, heat[i] + bumpPerClick);
    }

    void Update() {
        float t = 1f - Mathf.Exp(-smoothing * Time.deltaTime); // smooth catch-up

        for (int i = 0; i < layers.Length; i++) {
            FlameLayer layer = layers[i];

            heat[i] = Mathf.MoveTowards(heat[i], 0f, decayPerSecond * Time.deltaTime);
            shown[i] = Mathf.Lerp(shown[i], heat[i], t); // grow into the new size, don't snap

            // 0 until heat passes startHeat, then rises to 1 at full heat
            float bloom = Mathf.InverseLerp(layer.startHeat, 1f, shown[i]);

            layer.sprite.transform.localScale = baseScale[i] * Mathf.Lerp(layer.minScale, layer.maxScale, bloom);

            Color c = layer.sprite.color;
            c.a = Mathf.Lerp(layer.minAlpha, layer.maxAlpha, bloom);
            layer.sprite.color = c;
        }
    }
}