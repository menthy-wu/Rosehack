using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectbutton : MonoBehaviour
{
    
    [serializefield] string stage;
    private GameObject StageDisplay;
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
    }
    public void onPress()
    {
    }

}
