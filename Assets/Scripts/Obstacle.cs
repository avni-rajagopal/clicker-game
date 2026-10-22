using UnityEngine;
using TMPro;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public static bool active = false; // whether an obstacle is active (like the dialogue event)
    public int obstacleClicks; // how many clicks to get past the obstacle lol
    
    private int numClicks = 0; // how many times the player has clicked so far
    private int startClicks; // 
    public TMP_Text text; // the textbox that the associated dialogue event has
    public string lastLine; // a last line for bird to say when ballooning away
    
    public Transform balloonGroup; // objects that move up with the camera
    public float shakeAmount = 0.1f;
    // private Vector3[] originalPositions;
    private Vector3 originalPosition;

    public ParticleSystem particles;
    private Coroutine shakeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartObstacle() // called by DialogueEvent once it finishes maybe that's kinda dumb but w/e
    {
        active = true;
        Clickable.OnClick += OnClick;
    
        // originalPositions = new Vector3[balloonGroup.Length]; // save original position of balloonGroup bc otherwise shake displaces it after obstacle
        // for (int i=0; i<balloonGroup.Length; i++)
        //     originalPositions[i] = balloonGroup[i].position;
        originalPosition = balloonGroup.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick(int totalClicks)
    {
        Shake();
        // particles.Emit(20);
        StartCoroutine(EmitLeaves());
        numClicks++;
        Debug.Log(numClicks);
        if (numClicks >= obstacleClicks)
        {
            Invoke(nameof(FinishObstacle), 0.5f);   
        }
    
    }

    IEnumerator EmitLeaves()
    {
        for (int i = 0; i < 5; i++)
        {
            particles.Emit(1);
            yield return new WaitForSeconds(Random.Range(0.02f, 0.06f));
        }
    }

    public void Shake()
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        for (int j = 0; j < 5; j++) // number of little displacements to a shake
        {
            Vector3 offset = (Vector3)Random.insideUnitCircle * shakeAmount;
            balloonGroup.position = originalPosition + offset;
            yield return new WaitForSeconds(0.03f);
        }
        balloonGroup.position = originalPosition; 
    }

    void FinishObstacle()
    {
        Debug.Log("finished obstacle");
        Clickable.OnClick -= OnClick; // unsubscribe from event
        active = false;
        Debug.Log("obstacle active set to false");
        

        // actually display some random text after
        text.gameObject.SetActive(true);
        text.text = lastLine;

    }
}
