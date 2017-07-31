using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBaneling : MonoBehaviour {

    private  Animator anim;
    [HideInInspector]
    public GameObject HudTextGameObject;
    private HUDText hudText;

    public int MaxHp;
    [HideInInspector]
    public int hp;
    public string attackAudioName;
    public string hurtAudioName;
    public string dieAudioName;
    [HideInInspector]
    public GameObject HpBarGo;
    private UISlider hpBarSlider;

    private void Awake()
    {
        anim = transform.Find("Anim").GetComponent<Animator>();
        hp = MaxHp;
    }

    private void Start()
    {
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
        SoundManager._instance.Play(hurtAudioName, this.GetComponent<AudioSource>());
        if (hp <= 0)
        {
            Die();
        }
        HpBarGo.GetComponent<HpBar>().Show();
        hpBarSlider.value = (float)hp / MaxHp;
    }

    public void Die()
    {
        Destroy(HpBarGo);
        anim.SetTrigger("die");
        //Die Audio TODO
        SoundManager._instance.Play(dieAudioName, this.GetComponent<AudioSource>(), false, 0.3f);
        StartCoroutine(Dead());
    }

    //动画播放结束后处理
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
        GameManager._instance.EnemyList.Remove(this.gameObject);
        GameManager._instance.PlayLayerList.Remove(this.gameObject);
        Destroy(HudTextGameObject);
        Destroy(this.gameObject);
    }
}
