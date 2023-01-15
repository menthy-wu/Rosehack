using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject Credit;
    [SerializeField]
    GameObject Main;
    [SerializeField]
    GameObject Options;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void changeScene()
    {
        panel.SetActive(true);
         StartCoroutine(next());
    }
     IEnumerator next()
    {
     yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void back()
    {
        Credit.SetActive(false);
        Main.SetActive(true);
        Options.SetActive(false);
    }
    public void credit(){
        Credit.SetActive(true);
        Main.SetActive(false);
    }public void autoplay()
    {
        panel.SetActive(true);
        StartCoroutine(auto());
    }
     IEnumerator auto()
    {
     yield return new WaitForSeconds(1);
        SceneManager.LoadScene(5);
    }
    public void optionspage()
    {
        Options.SetActive(true);
        Main.SetActive(false);
    }
}
