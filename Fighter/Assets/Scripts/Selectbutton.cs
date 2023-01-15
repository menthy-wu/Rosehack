using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectbutton : MonoBehaviour
{

    [SerializeField]
    string stage;
    
    [SerializeField]
    int num;
    private GameObject StageDisplay;
    [SerializeField]
    GameStartButton GameStartButton;
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
    }
    public void onPress()
    {
        GameStartButton.selectedScene = num;
    }

}
