using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03DoorTrap : MonoBehaviour {
    private Animator anim;
    public static Level03DoorTrap _instance;
    private bool isOpen = false;
    [HideInInspector]
    public bool isActive;
    public float WaveTime;
    private float waveTimer;
    public GameObject EnergyWave;

    private void Awake()
    {
        _instance = this;
        anim = this.GetComponent<Animator>();
        isActive = false;
        waveTimer = WaveTime;
    }

    // Update is called once per frame
    void Update () {
        if (isActive)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer>=WaveTime)
            {
                anim.SetTrigger("active");
                waveTimer = 0;
            }
        }
	}

    public void ActiveTrap()
    {
        GameObject go = Instantiate(EnergyWave, transform.position, Quaternion.identity);
    }

    public void OpenDoor()
    {
        anim.SetTrigger("open");
        isOpen = true;
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isOpen)
        {
            VictoryMenu._instance.Show();
        }
    }
}
