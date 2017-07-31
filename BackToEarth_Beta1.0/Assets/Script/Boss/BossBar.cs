using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{

    private UISlider hpSlider;
    private UILabel hpLabel;
    private int MaxHp;
    public static BossBar _instance;

    void Awake()
    {
        hpSlider = transform.Find("HpProgressBar").GetComponent<UISlider>();
        hpLabel = transform.Find("Label").GetComponent<UILabel>();
        this.gameObject.SetActive(false);
        _instance = this;
    }

    public void InitBossBar(int maxHp)
    {
        MaxHp = maxHp;
        hpSlider.value = 1;
        hpLabel.text = maxHp + "/" + maxHp;
        this.gameObject.SetActive(true);
    }

    public void OnBossHpChanged(int currentHp)
    {
        hpSlider.value = (float)currentHp / MaxHp;
        hpLabel.text = currentHp + "/" + MaxHp;
    }

    public void DisappearBossBar()
    {
        this.gameObject.SetActive(false);
    }

}
