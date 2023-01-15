using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    
    [SerializeField]
    string stage;
    GameObject StageDisplay;
    bool selected;

    // Start is called before the first frame update
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
        if (stage == "bell")
        {
            selected = true;
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            selected = false;
            GetComponent<Image>().color = new Color(100, 100, 100, 255);
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

    public void setSelected(bool select)
    {
        selected = select;
        if(select)
        {
            
            // GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else{
            
            // GetComponent<Image>().color = new Color(100, 100, 100, 255);
        }
    }
}
