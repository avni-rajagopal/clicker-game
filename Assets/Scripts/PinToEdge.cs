using UnityEngine;

[ExecuteAlways]

public class PinToEdge : MonoBehaviour {
    
    public enum Side {Left, Right}
    public Side side = Side.Right; // pick in the inspector

    void LateUpdate() {
        Camera cam = Camera.main;

        if (cam == null) return;

        float halfScreen = cam.orthographicSize * cam.aspect;
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        float x = side == Side.Right
            ? cam.transform.position.x + halfScreen - halfWidth
            : cam.transform.position.x - halfScreen + halfWidth;

        Vector3 p = transform.position;
        transform.position = new Vector3(x, p.y, p.z);
    }
}