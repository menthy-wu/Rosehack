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
        Debug.Log(stage);
        if (stage == "hub")
        {
            currentStage = "hub";
            hub.SetActive(true);
            src.SetActive(false);
            bell.SetActive(false);
        }
        else if (stage == "src")
        {
            currentStage = "src";
            src.SetActive(true);
            hub.SetActive(false);
            bell.SetActive(false);
        }
        else if (stage == "bell")
        {
            currentStage = "bell";
            bell.SetActive(true);
            src.SetActive(false);
            hub.SetActive(false);
        }
    }
}
