using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameStartButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    int hubScene;

    [SerializeField]
    int srcScene;

    [SerializeField]
    int bellScene;
    public int selectedScene;
    soundEffects sound;

    void Start()
    {
        selectedScene = bellScene;
        sound = GameObject.Find("Sound").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update() { }

    public void onPress()
    {
        SceneManager.LoadScene(selectedScene);
        sound.playMusic("buttonClick");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {  
        
        sound.playMusic("buttonTrigger");
    }
}
