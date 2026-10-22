using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


public class Clickable : MonoBehaviour
{

    // This variable is marked as `static`, which means it is
    // accessible by any script, anywhere! They can just write
    // Clickable.Clicks to read/write to this value.
    //
    // See an example in `ClickTracker.cs`, which is on a UI
    // object titled `Tracker Text (TMP)`.
    public static int Clicks = 0;
    public float unitsPerClick = 2f; // controls how far the screen moves per click
    public static event Action<int> OnClick; // click event that other objects can read

    /// <summary>
    /// This function is called when the mouse button clicks
    /// on this object.
    /// </summary>
    private void OnMouseDown()
    {
        // if (TryGetComponent (out AudioSource sound)) {
        //     if (Random.Range(1,10)==1) sound.Play();
        // }
        Clicks += 1;  // add one point
        OnClick?.Invoke(Clicks); // broadcast click event
        
        if (!DialogueEvent.active && !Obstacle.active) // only update altitude when dialogue/obstacle isn't playing
        {
            Altitude.Value = Mathf.Min(Altitude.Value + unitsPerClick, Altitude.Max);
            Debug.Log("Altitude: " + Altitude.Value.ToString());
        }
        }
            

}
