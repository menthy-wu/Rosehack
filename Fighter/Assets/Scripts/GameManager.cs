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
    public string nextScene = "LevelSelection";

    // Start is called before the first frame update
    void Start()
    {
        entity1.SetActive(false);
        entity2.SetActive(false);

        allDialogues[0].TriggerDialogue();
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

        if (dialogueNum == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }

        entity1.SetActive(true);
        entity2.SetActive(true);

        // check if health is 0
        if (entity2.GetComponent<PlayerController>().health <= 0 && dialogueNum == 0)
        {
            dialogueNum = 1;
            allDialogues[1].TriggerDialogue();
        }
    }
}
