using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    private bool timeStopped;

    private float frameAdvanceHeldCounter;

    private float playerRespawnTimer;

    [SerializeField] private GameObject blackCover;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
    }

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // If the F key is pressed
        if(Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the time stopped variable
            //timeStopped = !timeStopped;
        }

        // If time is stopped
        if(timeStopped)
        {
            // Stop all delta time related things
            Time.timeScale = 0;
        }
        else
        {
            // Else return everything to normal speed
            Time.timeScale = 1.0f;
        }

        //if (Input.GetKey(KeyCode.Period) && timeStopped)
        //{
        //    frameAdvanceHeldCounter ++;
        //
        //    if (frameAdvanceHeldCounter >= 60f)
        //    {
        //        Time.timeScale = 1.0f;
        //    }
        //}

        // If the Period key is pressed, advance time for 1 frame (if time is stopped)
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Time.timeScale = 1.0f;
        }

        //if (Input.GetKeyUp(KeyCode.Period) && frameAdvanceHeldCounter >= 60f)
        //   frameAdvanceHeldCounter = 0.0f;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene("Game");
            Application.Quit();
        }

    }

    //Display FPS
    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;
    //
    //    GUIStyle style = new GUIStyle();
    //
    //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //    Rect rectFA = new Rect(0, h / 50, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 100;
    //    style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //    float msec = deltaTime * 1000.0f;
    //    float fps = 1.0f / deltaTime;
    //    string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    //    GUI.Label(rect, text, style);
    //
    //    if (timeStopped)
    //    {
    //        string textFA = string.Format("Frame Advance Enabled", msec, fps);
    //        GUI.Label(rectFA, textFA, style);
    //    }
    //}

    public bool PlayerDeathFadeOut()
    {
        blackCover.GetComponent<CanvasGroup>().alpha += Time.deltaTime;

        if(playerRespawnTimer >= 1.5f)
        {
            blackCover.GetComponent<CanvasGroup>().alpha = 1;
            return true;
        }

        playerRespawnTimer += Time.deltaTime;
        return false;
    }

    public bool PlayerDeathFadeIn()
    {
        blackCover.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;

        if (playerRespawnTimer >= 3f)
        {
            blackCover.GetComponent<CanvasGroup>().alpha = 0;
            playerRespawnTimer = 0;
            return true;
        }

        playerRespawnTimer += Time.deltaTime;

        return false;
    }
}
