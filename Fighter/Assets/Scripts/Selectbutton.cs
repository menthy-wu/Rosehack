using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] string stage;
    GameObject StageDisplay;
    bool selected;
    // Start is called before the first frame update
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
        if (stage == "bell")
        {
            selected = true;
        }
        else
        {
            selected = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onPress()
    {
        StageDisplay.GetComponent<StageDisplay>().setStage(stage);
    }
}
