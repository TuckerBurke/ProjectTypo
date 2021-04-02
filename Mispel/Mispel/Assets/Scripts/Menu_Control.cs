using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Control : MonoBehaviour
{
    private bool startPlayAnimation;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private Canvas canvas;

    private float buttonSpeed;
    private float fadeOutSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPlayAnimation = false;

        buttonSpeed = 5000.0f;
        fadeOutSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If the play button has been clicked
        if(startPlayAnimation)
        {
            // Move the play button to the left of the screen
            playButton.transform.Translate(-buttonSpeed*Time.deltaTime, 0, 0);
            // Move thr exit button to the right of the screen
            exitButton.transform.Translate(buttonSpeed * Time.deltaTime, 0, 0);
            // Fade out the background
            canvas.GetComponent<CanvasGroup>().alpha -= fadeOutSpeed * Time.deltaTime;

            // If the play button is off the screen
            if ((playButton.transform.position).x <= -900)
            {
                // Load the game scene
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void Play()
    {
        // Start the animation
        startPlayAnimation = true;
    }

    public void Exit()
    {
        // Close the game
        Application.Quit();
    }
}
