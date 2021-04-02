using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox;
    private Queue<string> lines;
    public bool dialogueStarted;
    public bool dialogueEnded;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
    }

    //Starts current dialogue and displays first line
    //Enables dialogie box UI
    public void StartDialogue (Dialogue dialogue)
    {
        //nameText.text = dialogue.name;

        lines.Clear();

        foreach (string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextLine();
        dialogueStarted = true;
        dialogueBox.SetActive(true);
    }

    //Displays next line of current dialogue
    //Returns true if dialogue has ended
    public bool DisplayNextLine()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayNPCTalk();

        if (lines.Count == 0)
        {
            EndDialogue();
            return true;
        }

        string line = lines.Dequeue();
        dialogueText.text = line;
        return false;
    }

    //Ends current dialogue
    //Disables dialogue box UI
    public void EndDialogue()
    {
        dialogueStarted = false;
        dialogueBox.SetActive(false);
        dialogueEnded = true;
    }
}
