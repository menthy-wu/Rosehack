using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour
{
    AudioSource buttonClick;
    AudioSource buttonTrigger;
    AudioSource bgm;
    static bool created = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!created)
        {
            DontDestroyOnLoad(gameObject);
            buttonClick = transform.Find("buttonClick").gameObject.GetComponent<AudioSource>();
            buttonTrigger = transform.Find("buttonTrigger").gameObject.GetComponent<AudioSource>();
            bgm = transform.Find("bgm").gameObject.GetComponent<AudioSource>();
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playMusic(string audioName)
    {
        if (audioName == "buttonClick")
            buttonClick.Play();
        else if (audioName == "buttonTrigger")
            buttonTrigger.Play();
    }
    public void adjustVolume(float value)
    {
        buttonClick.volume = value;
        buttonTrigger.volume = value;
        bgm.volume = value;
    }
}
