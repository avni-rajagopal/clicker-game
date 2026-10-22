using UnityEngine;

public class AltitudeGravity : MonoBehaviour {

    public float gravity = 2f; // how quickly falling speeds up (units per second, per second)
    public float maxFallSpeed = 5f; // falling never gets faster than this
    public float groundAltitude = 0f; // can't fall below this

    float fallSpeed = 0f;

    // Listen for clicks (same event Obstacle uses)
    void OnEnable() {Clickable.OnClick += HandleClick;}
    void OnDisable() {Clickable.OnClick -= HandleClick;}

    void HandleClick(int totalClicks) {
        fallSpeed = 0f; // a burst of fire stops the fall
    }

    void Update() {
        if (Altitude.Value >= Altitude.Max) return; // reached the top, stay there
        // Don't fall while dialogue or an obstacle is playing
        if (DialogueEvent.active || Obstacle.active) {
            fallSpeed = 0f;
            return;
        }

        fallSpeed = Mathf.Min(fallSpeed + gravity * Time.deltaTime, maxFallSpeed);
        Altitude.Value = Mathf.Max(groundAltitude, Altitude.Value - fallSpeed * Time.deltaTime);
    }
}