using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour {

    private UISlider energySlider;
    private UISlider hpSlider;
    private UILabel maxHpLabel;
    private UILabel hpLabel;

    void Awake()
    {
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        hpSlider = transform.Find("HpProgressBar").GetComponent<UISlider>();
        maxHpLabel = transform.Find("MaxHpLabel").GetComponent<UILabel>();
        hpLabel = transform.Find("CurrentHpLabel").GetComponent<UILabel>();
    }

    private void Start()
    {
        Tina._instance.OnTinaInfoChanged += this.OnTinaInfoChanged;
        hpLabel.text = Tina._instance.CurrentHp.ToString();
        maxHpLabel.text = "/" + Tina._instance.MaxHp.ToString();
    }

    private void OnDestroy()
    {
        Tina._instance.OnTinaInfoChanged -= this.OnTinaInfoChanged;
    }

    //主角信息发生改变时触发
    void OnTinaInfoChanged()
    {
        Tina tina = Tina._instance;
        energySlider.value = (float)tina.CurrentEnergy / tina.MaxEnergy;
        hpSlider.value = (float)tina.CurrentHp / tina.MaxHp;
        hpLabel.text = Tina._instance.CurrentHp.ToString();
    }
}
