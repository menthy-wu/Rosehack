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
    public GameObject viewCamera;

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
            // Reset camera position and fov
            viewCamera.transform.position = new Vector3(0, 0, -10);
            viewCamera.GetComponent<Camera>().orthographicSize = 5;
            if (Input.anyKeyDown)
            {
                dialogueManager.DisplayNextSentence();
            }
            return;
        }

        if (dialogueNum > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }

        entity1.SetActive(true);
        entity2.SetActive(true);

        // check if health is 0
        if (((entity2.GetComponent<EnemyController>() != null && entity2.GetComponent<EnemyController>().health <= 0) || (entity2.GetComponent<EnemyController>() == null && entity2.GetComponent<PlayerController>().health <= 0)) && dialogueNum == 0)
        {
            dialogueNum = 1;
            allDialogues[1].TriggerDialogue();
        }
        else if (((entity1.GetComponent<EnemyController>() != null && entity1.GetComponent<EnemyController>().health <= 0) || (entity1.GetComponent<EnemyController>() == null && entity1.GetComponent<PlayerController>().health <= 0)) && dialogueNum == 0)
        {
            dialogueNum = 2;
            allDialogues[2].TriggerDialogue();
        }

        if (dialogueNum == 0)
        {
            // set camera to zoom in and keep both entities in view
            viewCamera.transform.position = new Vector3((entity1.transform.position.x + entity2.transform.position.x) / 2, -2 + Mathf.Abs(entity1.transform.position.x - entity2.transform.position.x) / 4, -10);
            viewCamera.GetComponent<Camera>().orthographicSize = Mathf.Max(Mathf.Abs(entity1.transform.position.x - entity2.transform.position.x), Mathf.Abs(entity1.transform.position.y - entity2.transform.position.y)) / 4 + 2;
        }
    }
}
