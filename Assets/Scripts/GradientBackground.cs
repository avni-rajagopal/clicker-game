using UnityEngine;

[ExecuteAlways] // runs in the editor for live updates

[RequireComponent(typeof(SpriteRenderer))]

public class GradientBackground : MonoBehaviour {

    public Color bottomColor = new Color(0.55f, 0.8f, 1f); // light blue at ground
    public Color topColor = new Color(0.05f, 0.05f, 0.25f); // deep blue at night sky
    public float height = 200f; // world units

    const int Resolution = 256;
    Texture2D tex;
    bool edited = true;

    // runs when value is changed
    void OnValidate() {
        edited = true;
    }

    void LateUpdate() {
        if (edited) BuildGradient();

        Camera cam = Camera.main;
        if (cam == null) return;

        // width of camera = 2 * size * aspect
        float width = 2f * cam.orthographicSize * cam.aspect;
        transform.localScale = new Vector3(width, height / Resolution, 1f);
        transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
    }

    void BuildGradient() {
        edited = false;
        
        if (tex == null) {
            tex = new Texture2D(1, Resolution);
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.hideFlags = HideFlags.DontSave;
        }

        for (int y = 0; y < Resolution; y++) {
            tex.SetPixel(0, y, Color.Lerp(bottomColor, topColor, y / (float)(Resolution - 1)));
        }

        tex.Apply();

        // pivot at bottom-center, so the object's position marks the bottom edge
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, 1, Resolution), new Vector2(0.5f, 0f), 1f);
    }
}