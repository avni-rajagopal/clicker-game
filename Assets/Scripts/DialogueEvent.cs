using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueEvent : MonoBehaviour
{
    public static bool active; // means a dialogue scene is actively playing (tells altitude to stop updating)
    private bool triggered = false; // means this dialogue scene is actively playing
    private bool done = false;

    public float triggerAltitude; // altitude at which to trigger this dialogue
    public string[] lines; // dialogue to be said by the bird lol
    public TMP_Text text; // the text box dialogue is displayed in
    // public GameObject obstacle; // the obstacle to be started after dialogue finishes 
    public Obstacle obstacle;

    private int lineIndex = 0; // keeps track of index of current line being said

    public float letterDelay = 0.03f;
    private Coroutine typingCoroutine;
    private bool isTyping; // all the typewriter effect stuff

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered && Altitude.Value >= triggerAltitude && !done) 
        {
            StartDialogueEvent();
        }
        if (triggered && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                //finish displaying the current line immediately
                StopCoroutine(typingCoroutine);
                text.maxVisibleCharacters = int.MaxValue;
                isTyping = false;
            }
            else
            {
                if (lineIndex < lines.Length) 
                {
                    ShowLine();
                    lineIndex++;
                }
                else
                {
                    FinishDialogueEvent();
                }
            }
        }
    }

    void StartDialogueEvent()
    {
        // we've reached the triggerAltitude
        // need to make clicker unresponsive to clicks for the time being
        active = true;
        // Invoke(nameof(Start), 10f); // add a bit of time delay since ppl will be clicking fast lol
        triggered = true;
        text.gameObject.SetActive(true); // make text visible
    }

    void FinishDialogueEvent()
    {
        Debug.Log("finished");
        text.gameObject.SetActive(false);
        active = false;
        triggered = false;
        done = true;
        // now start the obstacle
        if (obstacle != null)
            obstacle.StartObstacle();
        
    }

    void ShowLine()
    {
        text.text = lines[lineIndex];
        text.maxVisibleCharacters = 0;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() // Coroutine for the typewriter effect, this part is copied lol
    {
        isTyping = true;
        foreach (char letter in lines[lineIndex])
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(letterDelay);
        }
        isTyping = false;
    }
}