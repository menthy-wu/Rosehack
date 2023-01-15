using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField]
    int hubScene;

    [SerializeField]
    int srcScene;

    [SerializeField]
    int bellScene;
    int selectedScene;

    void Start()
    {
        selectedScene = bellScene;
    }

    // Update is called once per frame
    void Update() { }

    public void onPress()
    {
        StartCoroutine(next());
    }
     IEnumerator next()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(selectedScene);
 
    }
}
