using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {
    public float ShowTime;
    private float showTimer=0;
    private bool isShow = false;

    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(isShow);
    }
	
	// Update is called once per frame
	void Update () {
        if (isShow)
        {
            showTimer += Time.deltaTime;
            if (showTimer>=ShowTime)
            {
                showTimer = 0;
                isShow = false;
                this.gameObject.SetActive(isShow);
            }
        }
	}

    public void Show()
    {
        isShow = true;
       this.gameObject.SetActive(isShow);
    }
}
