using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyLineType {
    Red,Green,Yellow,Blue,NotActived
}

public class EnergyLineTrap : MonoBehaviour {

    private GameObject EnergyLine;
    public EnergyLineType Type;

    private void Awake()
    {
        EnergyLine = transform.Find("EnergyLine").gameObject;
    }

    public void SetActive()
    {
        EnergyLine.SetActive(true);
    }

    public void SetDisActive()
    {
        EnergyLine.SetActive(false);
    }
}
