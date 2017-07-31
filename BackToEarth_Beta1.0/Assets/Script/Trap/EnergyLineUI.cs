using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLineUI : MonoBehaviour {

    private UILabel actived1Label;
    private UILabel actived2Label;
    public static EnergyLineUI _instance;

    private void Awake()
    {
        _instance = this;
        actived1Label = transform.Find("Actived1").GetComponent<UILabel>();
        actived2Label = transform.Find("Actived2").GetComponent<UILabel>();
        actived1Label.text = "——"; 
        actived1Label.color=Color.white;
        actived2Label.text = "——"; 
        actived2Label.color=Color.white;
    }

    public void ChangeActivedLabel(EnergyLineType actived1, EnergyLineType actived2)
    {
        switch (actived1)
        {
            case EnergyLineType.Red:
                actived1Label.text = "红色";
                actived1Label.color = Color.red;
                break;
            case EnergyLineType.Green:
                actived1Label.text = "绿色";
                actived1Label.color = Color.green;
                break;
            case EnergyLineType.Yellow:
                actived1Label.text = "黄色";
                actived1Label.color = Color.yellow;
                break;
            case EnergyLineType.Blue:
                actived1Label.text = "蓝色";
                actived1Label.color = Color.blue;
                break;
            case EnergyLineType.NotActived:
                actived1Label.text = "——";
                actived1Label.color = Color.white;
                break;
            default:
                break;
        }
        switch (actived2)
        {
            case EnergyLineType.Red:
                actived2Label.text = "红色";
                actived2Label.color = Color.red;
                break;
            case EnergyLineType.Green:
                actived2Label.text = "绿色";
                actived2Label.color = Color.green;
                break;
            case EnergyLineType.Yellow:
                actived2Label.text = "黄色";
                actived2Label.color = Color.yellow;
                break;
            case EnergyLineType.Blue:
                actived2Label.text = "蓝色";
                actived2Label.color = Color.blue;
                break;
            case EnergyLineType.NotActived:
                actived2Label.text = "——";
                actived2Label.color = Color.white;
                break;
            default:
                break;
        }
    }
}
