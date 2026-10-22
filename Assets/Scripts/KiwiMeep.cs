using UnityEngine;

public class KiwiMeep : MonoBehaviour
{
    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
    }
}