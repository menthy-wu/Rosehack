using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField]
    int num;
    [SerializeField]
    string stage;
    GameObject StageDisplay;
    bool selected;
    [SerializeField]
    GameStartButton start;

    soundEffects sound;
    // Start is called before the first frame update
    void Start()
    {
        StageDisplay = GameObject.Find("StageDisplay");
        sound = GameObject.Find("Sound").GetComponent<soundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onPress(string stage)
    {
        StageDisplay.GetComponent<StageDisplay>().setStage(stage);
        start.selectedScene = num;
        sound.playMusic("buttonClick");
    }

    public void setSelected(bool select)
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        sound.playMusic("buttonTrigger");
    }

}
