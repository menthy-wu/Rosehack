using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void back()
    {
        menuManager.GetComponent<MenuManager>().back();
    }
    public void credit()
    {
        menuManager.GetComponent<MenuManager>().credit();
    }
    public void auto()
    {
        menuManager.GetComponent<MenuManager>().autoplay();
    }
    public void backtomenu()
    {
        SceneManager.LoadScene(0);
    }
}
