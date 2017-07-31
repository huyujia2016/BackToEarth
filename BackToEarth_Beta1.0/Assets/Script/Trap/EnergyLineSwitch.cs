using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnergyLineSwitch : MonoBehaviour {

    private bool isOpened = false;
    private GameObject Mask;
    public EnergyLineType Type;

    private void Awake()
    {
        Mask = transform.Find("Mask").gameObject;
    }

    //开启开关，关闭能量线机关
    public void OpenSwitch()
    {
        isOpened = true;
        Mask.SetActive(false);
    }

    //关闭开关，打开能量线机关
    public void CloseSwitch()
    {
        isOpened = false;
        Mask.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isOpened == false)
        {
            SoundManager._instance.Play("getProp", this.transform.parent.gameObject.GetComponent<AudioSource>());
            EnergyLineSwitchManager._instance.SendMessage("DisActiveTrap", Type);
        }
    }
}
