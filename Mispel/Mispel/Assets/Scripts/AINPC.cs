using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINPC : Character
{
    GameObject player;
    private DialogueTrigger[] dialogues;
    [SerializeField] public bool bossDefeated;
    [SerializeField] public bool armsReceived;

    private bool playOrbAnim;

    // Make list in the future
    public bool eventTrigger;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        currentState = CharacterStates.Falling;
        player = GameObject.Find("Player");
        dialogues = GetComponents<DialogueTrigger>();
        availableForms.Add(Forms.Arms);
        facingRight = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (facingRight == true)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if(playOrbAnim)
        {
            //animator.Play("orb");
            //playOrbAnim = false;
        }
        else
        {
            //animator.Play("idle");
        }

        //If player is close enough to this NPC
        if (Mathf.Abs(transform.position.x-player.transform.position.x) < 2 && Mathf.Abs(transform.position.y-player.transform.position.y) < 0.5)
        {
            if(player.transform.position.x <= transform.position.x)
            {
                facingRight = false;
            }
            else
            {
                facingRight = true;
            }

            player.GetComponent<Player>().npcNearBy = true;

            //If the player presses the interact button
            if (player.GetComponent<Player>().ActionPressedDown)
            {
                //Start dialogue if not started already
                if (gameManager.GetComponent<DialogueManager>().dialogueStarted == false)

                    //Trigger different dialogue if boss is defeated
                    if (bossDefeated)
                        dialogues[2].TriggerDialogue();
                    //Trigger different dialogue if form has been received
                    else if (armsReceived)
                        dialogues[1].TriggerDialogue();
                    else
                        dialogues[0].TriggerDialogue();
                //Otherwise continue to next line
                else
                    gameManager.GetComponent<DialogueManager>().DisplayNextLine();

                if (armsReceived == false && gameManager.GetComponent<DialogueManager>().dialogueEnded == true)
                {
                    armsReceived = true;
                    player.GetComponent<Player>().availableForms.Clear();
                    player.GetComponent<Player>().availableForms.Add(Forms.Arms);
                    availableForms.Clear();
                    availableForms.Add(Forms.Base);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayPlayerGetNewForm();
                    playOrbAnim = true;
                }
            }
        }
        else
        {
            //animator.Play("idle");
            player.GetComponent<Player>().npcNearBy = false;
            if (gameManager.GetComponent<DialogueManager>().dialogueStarted)
            {
                gameManager.GetComponent<DialogueManager>().EndDialogue();
                gameManager.GetComponent<DialogueManager>().dialogueEnded = false;
            }
        }


    }
}
