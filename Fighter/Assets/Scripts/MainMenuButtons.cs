using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    GameObject menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("MenuManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        menuManager.GetComponent<MenuManager>().changeScene();
    }
}
