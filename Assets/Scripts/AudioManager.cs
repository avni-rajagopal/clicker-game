using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource play;
    public AudioSource fadeTo;
    public AudioClip[] sounds;
    public float[] startAlts;
    public float fadeSpeed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        play.clip = sounds[0];
        play.Play();
    }

    // Update is called once per frame
    void Update()
    {
        AudioClip toPlay = sounds[0];
        for (int i = 0; i < sounds.Length; i++) {
            if (Altitude.Value >= startAlts[i]) toPlay = sounds[i];
        }

        if (play.clip != toPlay) {
            AudioSource old = play;
            play = fadeTo;
            fadeTo = old;

            play.clip = toPlay;
            play.volume = 0;
            play.Play();
        }

        play.volume = Mathf.MoveTowards(play.volume, 1, fadeSpeed * Time.deltaTime);
        fadeTo.volume = Mathf.MoveTowards(fadeTo.volume, 0, fadeSpeed * Time.deltaTime);
    }
}
