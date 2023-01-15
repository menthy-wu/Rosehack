using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    [SerializeField]
    int hubScene;

    [SerializeField]
    int srcScene;

    [SerializeField]
    int bellScene;
    public int selectedScene;

    void Start()
    {
        selectedScene = bellScene;
    }

    // Update is called once per frame
    void Update() { }

    public void onPress()
    {
        SceneManager.LoadScene(selectedScene);
    }
}
