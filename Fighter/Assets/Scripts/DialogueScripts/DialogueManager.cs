using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // new textmeshpro text
    public TextMeshPro nameText;
    public TextMeshPro dialogueText;

    public Animator animator;

    private Queue<string> sentences = new Queue<string>();
    public bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        isTalking = true;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string Sentence)
    {
        dialogueText.text = "";
        foreach (char letter in Sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        isTalking = false;
        animator.SetBool("IsOpen", false);
    }
}
