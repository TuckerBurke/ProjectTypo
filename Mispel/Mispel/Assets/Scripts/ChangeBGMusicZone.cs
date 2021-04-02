using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGMusicZone : MonoBehaviour
{
    private bool hasTriggered;

    private SoundManager soundManager;
    [SerializeField] private AudioClip trackToPlay;

    private float leftZoneTimer;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in the music zone and the player didn't just enter a new zone
        if(hasTriggered && leftZoneTimer >= 0.5f)
        {
            // Set the background music to the track in the zone
            soundManager.backgroundMusicPlayer.clip = trackToPlay;
            // If the song isn't already playing
            if (soundManager.backgroundMusicPlayer.isPlaying == false)
            {
                // Play it
                soundManager.backgroundMusicPlayer.Play();
            }
        }

        // Increase the timer
        leftZoneTimer += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the player is in the music zone
        if(collision.transform.root.name == "Player" && collision.transform.root.gameObject.GetComponent<Character>().GroundColliderBox == collision)
        {
            hasTriggered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player first enters the zone, reset the timer
        leftZoneTimer = 0.0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the player leaves the music zone
        if (collision.transform.root.name == "Player" && collision.transform.root.gameObject.GetComponent<Character>().GroundColliderBox == collision)
        {
            hasTriggered = false;
            // If the current track is not the same as the track in the zone
            if (soundManager.backgroundMusicPlayer.clip != trackToPlay)
            {
                // Stop the current one
                soundManager.backgroundMusicPlayer.Stop();
            }
        }
    }
}
