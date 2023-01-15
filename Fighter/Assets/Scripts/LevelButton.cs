using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    
    [SerializeField]
    int num;
    [SerializeField]
    string stage;
    GameObject StageDisplay;
    bool selected;
    [SerializeField]
    GameStartButton start;

    // Start is called before the first frame update
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onPress()
    {
        StageDisplay.GetComponent<StageDisplay>().setStage(stage);
        start.selectedScene = num;
    }

    public void setSelected(bool select)
    {
    }
}
