using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBaneling :MonoBehaviour {

    private bool isActivated = false;
    public float moveSpeed;
    private GameObject tina;
    private  Animator anim;
    //爆炸伤害
    public int damage;
    //血量
    public int MaxHp;
    public int hp;
    //移动距离
    public float MoveDistance;

    private GameObject HudTextGameObject;
    private HUDText hudText;
    private Vector2 explodePos;
    private bool findExplodePos = false;
    private float _moveTime;//移动到目标点需要的时间
    private float _timeCount = 0;//已经经过的时间
    private Vector2 startPos;
    [HideInInspector]
    public GameObject HpBarGo;
    private UISlider hpBarSlider;

    private bool isCollidered;

    private void Start()
    {
        tina = GameManager._instance.Player;
        anim= transform.Find("Anim").GetComponent<Animator>();
        HudTextGameObject = GameManager._instance.GetHudText(transform.Find("HpPoint").gameObject);
        hudText = HudTextGameObject.GetComponent<HUDText>();
        _moveTime = MoveDistance / moveSpeed;
        HpBarGo = GameManager._instance.GetHpBar(transform.Find("HpPoint").gameObject);
        hpBarSlider = HpBarGo.transform.Find("Bg").GetComponent<UISlider>();
        isCollidered = false;
    }

   

    public void Update () {

        if (isActivated)
        {
            if (findExplodePos == false)
            {
                startPos = transform.position;
                explodePos.x = transform.position.x - MoveDistance * (transform.position.x- tina.transform.position.x) / (float)Math.Sqrt(Math.Pow(tina.transform.position.x - transform.position.x, 2)+ Math.Pow(tina.transform.position.y - transform.position.y, 2));
                explodePos.y = transform.position.y- MoveDistance * (transform.position.y - tina.transform.position.y)/ (float)Math.Sqrt(Math.Pow(tina.transform.position.x - transform.position.x, 2)+ Math.Pow(tina.transform.position.y - transform.position.y, 2));
                findExplodePos = true;
            }
            else
            {
                float distance = Vector2.Distance(explodePos, transform.position);

                if (distance < 0.35f)
                {
                    Explode();
                }
                else
                {
                    _timeCount += Time.deltaTime;
                    transform.position = Vector2.Lerp(startPos, explodePos, _timeCount / _moveTime);
                    anim.SetTrigger("roll");
                }
                
            }
        }
    }

    public void Die()
    {
        Explode();
    }

    private void Explode()
    {
        isActivated = false;
        SoundManager._instance.Play("banelingExplode", this.GetComponent<AudioSource>());
        anim.SetTrigger("explodsion");
        float distance = Vector2.Distance(tina.transform.position, transform.position);
        if (distance<1.3f)
        {
            tina.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        StartCoroutine(Dead());
    }

    //动画播放结束后处理
    IEnumerator Dead()
    {
        Destroy(HpBarGo);
        yield return new WaitForSeconds(1.5f);
        GameManager._instance.EnemyList.Remove(this.gameObject);
        GameManager._instance.PlayLayerList.Remove(this.gameObject);
        this.transform.parent.GetComponent<EnemyTrigger>().ActivationList.Remove(this.gameObject);
        Destroy(HudTextGameObject);
        Destroy(this.gameObject);
    }

    void TakeDamage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;
        hudText.Add("-" + damage, Color.yellow, 0.3f);
        if (hp <= 0)
        {
            Die();
        }
        HpBarGo.GetComponent<HpBar>().Show();
        hpBarSlider.value = (float)hp / MaxHp;
    }

    //接受触发器的激活命令
    public void Activate()
    {
        isActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Ground" && isCollidered==false)
        {
            isCollidered = true;
            Explode();
        }
    }
}
