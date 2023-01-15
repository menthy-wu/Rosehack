using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject targetEntity = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetEntity.GetComponent<EnemyController>() != null)
            // change width of rectTransform
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(targetEntity.GetComponent<EnemyController>().health * 4, 100);
        else
        {
            // change width of rectTransform
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(targetEntity.GetComponent<PlayerController>().health * 4, 100);
        }
    }
}
