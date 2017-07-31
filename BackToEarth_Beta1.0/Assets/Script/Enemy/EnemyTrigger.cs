using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    private bool isTriggered = false;
    //[HideInInspector]
    public List<GameObject> ActivationList;

    private void Awake()
    {
        ActivationList = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            ActivationList.Add(transform.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isTriggered == false)
        {
            isTriggered = true;
            foreach (GameObject go in ActivationList)
            {
                go.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
