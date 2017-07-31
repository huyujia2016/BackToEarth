using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public abstract class EnemyBase : MonoBehaviour {

    private PolyNavAgent _agent;
    [HideInInspector]
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    private GameObject _tina;
    [HideInInspector]
    public GameObject tina
    {
        get
        {
            if (!_tina)
                _tina = Tina._instance.gameObject;
            return _tina;
        }
    }

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public GameObject HudTextGameObject;
    private HUDText hudText;
    public string runAudioName;
    public string hurtAudioName;
    [HideInInspector]
    public GameObject HpBarGo;
    private UISlider hpBarSlider;

    //移动方向
    public MoveDirection direction;
    //移动速度
    public float moveSpeed;
    //攻击距离
    public float attackDistance;
    //攻击频率
    public float attackRate = 3;
    private float attackRatetimer = 0;
    //攻击伤害
    public int damage;
    //血量
    public int MaxHp;
    [HideInInspector]
    public int hp;

    [HideInInspector]
    public bool isActivated = false;


    private void Start()
    {
        hp = MaxHp;
        anim = transform.Find("Anim").GetComponent<Animator>();
        HudTextGameObject = GameManager._instance.GetHudText(transform.Find("HpPoint").gameObject);
        hudText = HudTextGameObject.GetComponent<HUDText>();
        attackRatetimer = attackRate;
        HpBarGo = GameManager._instance.GetHpBar(transform.Find("HpPoint").gameObject);
        hpBarSlider = HpBarGo.transform.Find("Bg").GetComponent<UISlider>();
    }

    public virtual void Update()
    {
        if (isActivated)
        {
            float distance = Vector2.Distance(tina.transform.position, transform.position);
            if (distance < attackDistance)
            {
                if (direction == MoveDirection.Left)
                {
                    anim.SetTrigger("left_idle");
                }
                else
                {
                    anim.SetTrigger("right_idle");
                }
                attackRatetimer += Time.deltaTime;
                if (attackRatetimer >= attackRate)
                {
                    attackRatetimer = 0;
                    if (direction == MoveDirection.Left)
                    {
                        anim.SetTrigger("left_attack");
                    }
                    else
                    {
                        anim.SetTrigger("right_attack");
                    }
                }
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
                {
                    //追主角
                    MoveToTina();
                }
            }
        }
       
    }

    public void MoveToTina()
    {
        agent.maxSpeed = moveSpeed;
        agent.SetDestination(tina.transform.position);
        if (tina.transform.position.x > this.transform.position.x)
        {
            direction = MoveDirection.Right;
            anim.SetTrigger("right_run");
        }
        else
        {
            direction = MoveDirection.Left;
            anim.SetTrigger("left_run");
        }
        SoundManager._instance.Play(runAudioName, this.GetComponent<AudioSource>(), false,0.3f);
    }


    public virtual void Die()
    {
        if (direction == MoveDirection.Left)
        {
            anim.SetTrigger("left_die");
        }
        else
        {
            anim.SetTrigger("right_die");
        }
        isActivated = false;
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

    public void Activate()
    {
        isActivated = true;
    }
}
