using System.Collections;
using UnityEngine;

public class EndingSequence : MonoBehaviour {
    public float triggerAltitude = 180f;
    public float panToY = 190f; // camera Y where the screen is full of stars
    public float panDuration = 4f; // seconds
    public Scrolling scrolling; // turned off so the camera can move on its own

    [Header("Fade (leave empty to end on the stars)")]
    public CanvasGroup blackScreen;
    public float fadeDuration = 2f;

    bool started = false;

    void Update() {
        if (!started && Altitude.Value >= triggerAltitude) {
            started = true;
            StartCoroutine(EndingRoutine());
        }
    }

    IEnumerator EndingRoutine() {
        scrolling.enabled = false; // camera stops following altitude, balloon stays behind
        Transform cam = scrolling.transform;

        // smooth pan up to the stars
        float startY = cam.position.y;
        for (float t = 0f; t < 1f; t += Time.deltaTime / panDuration) {
            float y = Mathf.Lerp(startY, panToY, Mathf.SmoothStep(0f, 1f, t));
            cam.position = new Vector3(cam.position.x, y, cam.position.z);
            yield return null;
        }
        cam.position = new Vector3(cam.position.x, panToY, cam.position.z);

        yield return new WaitForSeconds(3f); // linger on the stars

        if (blackScreen != null) yield return Fade(blackScreen);
    }

    IEnumerator Fade(CanvasGroup group) {
        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeDuration) {
            group.alpha = t;
            yield return null;
        }
        group.alpha = 1f;
    }
}