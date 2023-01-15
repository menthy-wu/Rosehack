using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueTrigger[] allDialogues;
    public DialogueManager dialogueManager;
    public int dialogueNum = 0;
    public GameObject entity1;
    public GameObject entity2;

    // Start is called before the first frame update
    void Start()
    {
        entity1.SetActive(false);
        entity2.SetActive(false);

        allDialogues[dialogueNum].TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.isTalking)
        {
            if (Input.anyKeyDown)
            {
                dialogueManager.DisplayNextSentence();
            }
            return;
        }

        entity1.SetActive(true);
        entity2.SetActive(true);
    }
}
