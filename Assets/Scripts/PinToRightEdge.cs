using UnityEngine;

[ExecuteAlways]

public class PinToRightEdge : MonoBehaviour
{
    public float padding = 0.0f; // no padding

    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float rightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        Vector3 p = transform.position;
        transform.position = new Vector3(rightEdge - halfWidth - padding, p.y, p.z);
    }
}