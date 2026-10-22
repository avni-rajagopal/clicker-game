using UnityEngine;

/// <summary>
/// Scrolls the screen upward as the player clicks.
///
/// Put this on the Main Camera. Every click raises
/// `Altitude.Value` by 1 (see `Clickable.cs`), and this script
/// glides the camera up to match. Anything in `riders` (e.g. the
/// BalloonGroup) moves along with the camera so it stays on
/// screen while the world scrolls past.
/// </summary>
public class Scrolling : MonoBehaviour
{
    public float unitsPerClick = 0.25f; // how far the screen moves per click (world units)
    public float smoothTime = 0.2f;     // seconds to catch up; 0 = snap instantly
    public Transform[] riders;          // objects that move up with the camera

    float startY;
    float velocity;

    void Start()
    {
        startY = transform.position.y;
    }

    void LateUpdate()
    {
        float targetY = startY + Altitude.Value * unitsPerClick;
        float newY = smoothTime > 0f
            ? Mathf.SmoothDamp(transform.position.y, targetY, ref velocity, smoothTime)
            : targetY;

        // move riders by the same amount as the camera so they keep their place on screen
        float deltaY = newY - transform.position.y;
        foreach (Transform rider in riders)
        {
            if (rider != null) rider.position += new Vector3(0f, deltaY, 0f);
        }

        Vector3 p = transform.position;
        transform.position = new Vector3(p.x, newY, p.z);
    }
}
