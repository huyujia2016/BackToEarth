using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObstacle : MonoBehaviour {

    [HideInInspector]
    public GameObject HudTextGameObject;
    private HUDText hudText;

    public int MaxHp;
    [HideInInspector]
    public int hp;
    public string hurtAudioName;
    public string dieAudioName;

    [HideInInspector]
    public GameObject HpBarGo;
    private UISlider hpBarSlider;
    public GameObject NavObstacle;
    public GameObject Pots;

    // Use this for initialization
    void Start () {
        hp = MaxHp;
        HpBarGo = GameManager._instance.GetHpBar(transform.Find("HpPoint").gameObject);
        hpBarSlider = HpBarGo.transform.Find("Bg").GetComponent<UISlider>();

        HudTextGameObject = GameManager._instance.GetHudText(transform.Find("HpPoint").gameObject);
        hudText = HudTextGameObject.GetComponent<HUDText>();
    }

    void TakeDamage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;
        hudText.Add("-" + damage, Color.yellow, 0.3f);
        SoundManager._instance.Play(hurtAudioName, SoundManager._instance.GetComponent<AudioSource>());
        if (hp <= 0)
        {
            Die();
        }
        HpBarGo.GetComponent<HpBar>().Show();
        hpBarSlider.value = (float)hp / MaxHp;
    }

    public void Die()
    {
        Pots.SetActive(true);
        Destroy(HpBarGo);
        Destroy(NavObstacle);
        SoundManager._instance.Play(dieAudioName, SoundManager._instance.GetComponent<AudioSource>(), false, 0.3f);
        GameManager._instance.EnemyList.Remove(this.gameObject);
        GameManager._instance.PlayLayerList.Remove(this.gameObject);
        Destroy(HudTextGameObject);
        Destroy(this.gameObject);
    }


}
