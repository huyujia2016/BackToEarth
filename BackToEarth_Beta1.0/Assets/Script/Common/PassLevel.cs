using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevel : MonoBehaviour {

    public GameObject PassTrigger;

    private void Update()
    {
        if (this.gameObject.GetComponent<EnemyTrigger>().ActivationList.Count==0)
        {
            PassTrigger.SetActive(true);
        }
    }
}
