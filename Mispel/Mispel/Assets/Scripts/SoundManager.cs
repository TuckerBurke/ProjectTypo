using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip playerMissGround, playerMissAir, playerHitGround, playerHitAir, playerJump, playerDoubleJump, wallCrumble, playerTransform, playerWalk, playerGetNewForm, npcTalk, doorClose, tutorial1Music, tutorial1To2Transition, tutorial2Music, bossMusic, playerFireAttack, playerArmAttack;
    public AudioSource backgroundMusicPlayer;
    [SerializeField] private AudioSource playerSFXPlayer;
    [SerializeField] private AudioSource environmentSFXPlayer;
    [SerializeField] private AudioSource npcSFXPlayer;

    public bool PlayerSFXIsPlaying
    {
        get { return playerSFXPlayer.isPlaying; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerAttackMissGround()
    {
        playerSFXPlayer.clip = playerMissGround;
        playerSFXPlayer.Play();
    }

    public void PlayPlayerAttackMissAir()
    {
        playerSFXPlayer.clip = playerMissAir;
        playerSFXPlayer.Play();
    }

    public void PlayPlayerJump()
    {
        playerSFXPlayer.clip = playerJump;
        playerSFXPlayer.Play();
    }

    public void PlayPlayerDoubleJump()
    {
        playerSFXPlayer.clip = playerDoubleJump;
        playerSFXPlayer.Play();
    }

    public void PlayWallCrumble()
    {
        environmentSFXPlayer.clip = wallCrumble;
        environmentSFXPlayer.Play();
    }

    public void PlayPlayerChangeForm()
    {
        playerSFXPlayer.clip = playerTransform;
        playerSFXPlayer.Play();
    }

    public void PlayPlayerWalk()
    {
        playerSFXPlayer.clip = playerWalk;
        playerSFXPlayer.Play();
    }

    public void StopPlayerWalk()
    {
        if(playerSFXPlayer.clip == playerWalk)
            playerSFXPlayer.Stop();
    }

    public void PlayPlayerGetNewForm()
    {
        playerSFXPlayer.clip = playerGetNewForm;
        playerSFXPlayer.Play();
    }

    public void PlayNPCTalk()
    {
        npcSFXPlayer.clip = npcTalk;
        npcSFXPlayer.Play();
    }

    public void PlayDoorClose()
    {
        environmentSFXPlayer.clip = doorClose;
        environmentSFXPlayer.Play();
    }

    public void PlayPlayerFireAttack()
    {
        playerSFXPlayer.clip = playerFireAttack;
        playerSFXPlayer.Play();
    }

    public void PlayPlayerArmAttack()
    {
        playerSFXPlayer.clip = playerArmAttack;
        playerSFXPlayer.Play();
    }
}
