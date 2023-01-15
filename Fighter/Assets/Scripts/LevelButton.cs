using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    GameObject StageDisplay;
    public bool selected;
    public GameObject otherbutton1;
    public GameObject otherbutton2;

    // Start is called before the first frame update
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onPress(string stage)
    {
        StageDisplay.GetComponent<StageDisplay>().setStage(stage);
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        otherbutton1.GetComponent<Image>().color = new Color(100, 100, 100, 255);
        otherbutton2.GetComponent<Image>().color = new Color(100, 100, 100, 255);
    }

}
