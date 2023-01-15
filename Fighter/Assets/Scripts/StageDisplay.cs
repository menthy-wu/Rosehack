using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDisplay : MonoBehaviour
{
    GameObject hub;
    GameObject src;
    GameObject bell;
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
    void Update()
    {

    }
    public void setStage(string stage)
    {
        if (stage == "hub")
        {
            hub.SetActive(true);
            src.SetActive(false);
            bell.SetActive(false);
            currentStage = "hub";
        }
        else if (stage == "src")
        {

            src.SetActive(true);
            hub.SetActive(false);
            bell.SetActive(false);
            currentStage = "src";
        }
        else if (stage == "bell")
        {

            src.SetActive(true);
            currentStage = "bell";
            bell.SetActive(false);
            hub.SetActive(false);
        }
    }
}
