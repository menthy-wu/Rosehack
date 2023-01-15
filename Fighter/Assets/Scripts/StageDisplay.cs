using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDisplay : MonoBehaviour
{
    GameObject hub;
    GameObject src;
    GameObject bell;
    [SerializeField]
    GameObject hubButton;
    [SerializeField]
    GameObject srcButton;
    [SerializeField]
    GameObject bellButton;

    string currentStage = "bell";

    // Start is called before the first frame update
    void Start()
    {
        hub = transform.Find("hub").gameObject;
        src = transform.Find("src").gameObject;
        bell = transform.Find("bell").gameObject;

        hub.SetActive(false);
        src.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    public void setStage(string stage)
    {
        if (stage == "hub")
        {
            hub.SetActive(true);
            src.SetActive(false);
            bell.SetActive(false);
            currentStage = "hub";
            hubButton.GetComponent<LevelButton>().setSelected(true);
            srcButton.GetComponent<LevelButton>().setSelected(false);
            bellButton.GetComponent<LevelButton>().setSelected(false);
        }
        else if (stage == "src")
        {
            src.SetActive(true);
            hub.SetActive(false);
            bell.SetActive(false);
            currentStage = "src";
            hubButton.GetComponent<LevelButton>().setSelected(false);
            srcButton.GetComponent<LevelButton>().setSelected(true);
            bellButton.GetComponent<LevelButton>().setSelected(false);
        }
        else if (stage == "bell")
        {
            bell.SetActive(true);
            currentStage = "bell";
            src.SetActive(false);
            hub.SetActive(false);
            hubButton.GetComponent<LevelButton>().setSelected(false);
            srcButton.GetComponent<LevelButton>().setSelected(false);
            bellButton.GetComponent<LevelButton>().setSelected(false);
        }
    }
}
