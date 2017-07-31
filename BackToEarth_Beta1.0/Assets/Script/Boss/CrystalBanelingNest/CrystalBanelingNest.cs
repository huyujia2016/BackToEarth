using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBanelingNest : MonoBehaviour {

    private Animator anim;
    private GameObject HudTextGameObject;
    private HUDText hudText;
    private PolyNavAgent agent;
    
    //音效
    public string attackAudioName;
    public string hurtAudioName;
    public string dieAudioName;
    public string runAudioName;

    public int MaxHp;
   // [HideInInspector]
    public int hp;
    public float MoveSpeed = 2;
    [HideInInspector]
    public MoveDirection direction;
    private Transform player;

    //P2增加能量环技能
    private bool P2 = false;
    //P3能量环取消，召唤黄色毒爆虫
    private bool P3 = false;
    //P4召唤黄色毒爆虫，有能量环
    private bool P4 = false;
    //P5召唤绿色毒爆虫，有能量环
    private bool P5 = false;
        
    //远程技能参数
    public GameObject Shell;
    public float EmissionIntervalPerWave;//每个子弹间隔时间
    private float emissionIntervalPerWaveTimer;
    public float WaveInterval;//每波间隔时间
    private float waveIntervalTimer = 0;
    public int EmissionTimes;//一波发射子弹个数
    private int emissionTime = 0;
    //近战技能参数
    public float MeleeAttackRate;//多少秒攻击一次
    [HideInInspector]
    public float meleeAttackTimer;
    public int MeleeDamage;

    //召唤黄毒爆技能参数
    public int SummonNumber;
    public GameObject WarnArea;
    public GameObject YellowBanelings;
    public int LandingDamage;
    public float SummonInterval;
    private float SummonIntervalTimer;

    //召唤绿毒爆参数
    public GameObject GreenBanelings;


    public static CrystalBanelingNest _instance;
    private void Awake()
    {
        
        _instance = this;
        anim = transform.Find("Anim").GetComponent<Animator>();
        hp = MaxHp;
        agent= GetComponent<PolyNavAgent>();
    }

    // Use this for initialization
    void Start () {
        HudTextGameObject = GameManager._instance.GetHudText(transform.Find("HpPoint").gameObject);
        hudText = HudTextGameObject.GetComponent<HUDText>();
        player = GameManager._instance.Player.transform;
        meleeAttackTimer = MeleeAttackRate;
    }

    private bool isActived = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActived==false)
        {
            isActived = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (isActived)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            meleeAttackTimer += Time.deltaTime;
            if (distance > 3.5f)
            {
                waveIntervalTimer += Time.deltaTime;
                if (waveIntervalTimer < WaveInterval)
                {
                    if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
                    {
                        //追主角
                        MoveToTina();
                    }
                }
                else
                {
                    agent.Stop();
                    RemoteAttack();
                }
            }
            else if (distance > 1.5f)
            {
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
                {
                    //追主角
                    MoveToTina();
                }
            }
            else
            {
                agent.Stop();
                MeleeAttack();
            }

            //P5增加召唤绿色毒爆技能
            if (P5)
            {
                SummonIntervalTimer += Time.deltaTime;
                if (SummonIntervalTimer >= SummonInterval)
                {
                    SummonIntervalTimer = 0;
                    SummonBanelings(GreenBanelings, "summon_green_baneling(Clone)");
                }
            }
            //P3增加召唤黄色毒爆技能
            else if (P3)
            {
                SummonIntervalTimer += Time.deltaTime;
                if (SummonIntervalTimer >= SummonInterval)
                {
                    SummonIntervalTimer = 0;
                    SummonBanelings(YellowBanelings, "summon_yellow_baneling(Clone)");
                }
            }
        }
    }

    public void MoveToTina()
    {
        agent.maxSpeed = MoveSpeed;
        agent.SetDestination(player.position);
        if (player.position.x > this.transform.position.x)
        {
            anim.SetTrigger("right_run");
        }
        else
        {
            anim.SetTrigger("left_run");
        }
        SoundManager._instance.Play(runAudioName, this.GetComponent<AudioSource>(), false, 0.3f);
    }

    

    void SummonBanelings(GameObject Banelings,string BanelingName)
    {
        for (int i = 0; i < SummonNumber; i++)
        {
            float x = Random.Range(-6F, 6F);
            float y = Random.Range(-4F, 3.3F);
            Vector2 pos = new Vector2(x, y);
            GameObject go = Instantiate(WarnArea, pos, Quaternion.identity);
            go.GetComponent<LandingDamageTrigger>().AcceptObjectName = BanelingName;
            Instantiate(Banelings, new Vector2(go.transform.position.x,6), Quaternion.identity);
        }
    }


    //近战攻击
    void MeleeAttack()
    {
        if (meleeAttackTimer >= MeleeAttackRate)
        {
            if (player.position.x > this.transform.position.x)
            {
                direction = MoveDirection.Right;
                anim.SetTrigger("right_attack");
            }
            else
            {
                direction = MoveDirection.Left;
                anim.SetTrigger("left_attack");
            }
            meleeAttackTimer = 0;
        }
        else
        {
            if (player.position.x > this.transform.position.x)
            {
                anim.SetTrigger("right_idle");
            }
            else
            {
                anim.SetTrigger("left_idle");
            }
        }
    }

    //远程攻击
    void RemoteAttack()
    {
        emissionIntervalPerWaveTimer += Time.deltaTime;
        if (emissionIntervalPerWaveTimer >= EmissionIntervalPerWave)
        {
            if (emissionTime < EmissionTimes)
            {
                if (player.position.x > this.transform.position.x)
                {
                    anim.SetTrigger("right_remoteAttack");
                }
                else
                {
                    anim.SetTrigger("left_remoteAttack");
                }

                GameObject go = Instantiate(Shell, transform.Find("Anim/EmissionPoint").position, Quaternion.identity);
                emissionIntervalPerWaveTimer = 0;
                emissionTime++;
            }
            else
            {
                emissionTime = 0;
                waveIntervalTimer = 0;
                if (player.position.x > this.transform.position.x)
                {
                    anim.SetTrigger("right_run");
                }
                else
                {
                    anim.SetTrigger("left_run");
                }
            }
        }
    }

    private bool isShowBossBar=false;

    //受到伤害
    void TakeDamage(int damage)
    {
        if (isShowBossBar== false)
        {
            BossBar._instance.InitBossBar(MaxHp);
            isShowBossBar = true;
        }
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;

        //P2增加能量环技能
        if (hp <= 270 && P2 == false)
        {
            P2 = true;
            Level03DoorTrap._instance.isActive = true;
        }
        //P3能量环取消，召唤黄色毒爆虫
        if (hp <= 210 && P3 == false)
        {
            SummonIntervalTimer = SummonInterval;
            P3 = true;
            Level03DoorTrap._instance.isActive = false;
        }
        //P4召唤黄色毒爆虫，有能量环
        if (hp <= 150)
        {
            P4 = true;
            Level03DoorTrap._instance.isActive = true;
        }
        //P5召唤绿色毒爆虫，有能量环
        if (hp <= 60 && P5 == false)
        {
            SummonIntervalTimer = SummonInterval;
            P3 = false;
            P5 = true;
        }

        BossBar._instance.OnBossHpChanged(hp);
        hudText.Add("-" + damage, Color.yellow, 0.3f);
        SoundManager._instance.Play(hurtAudioName, this.GetComponent<AudioSource>());
        if (hp <= 0)
        {
            Die();
        }
    }

    //死亡
    public void Die()
    {
        if (player.position.x > this.transform.position.x)
        {
            anim.SetTrigger("right_die");
        }
        else
        {
            anim.SetTrigger("left_die");
        }
        //Die Audio TODO
        SoundManager._instance.Play(dieAudioName, this.GetComponent<AudioSource>(), false, 0.3f);
        P2 = false;
        P3 = false;
        P4 = false;
        P5 = false;
        Level03DoorTrap._instance.OpenDoor();
        Destroy(BossBar._instance.gameObject);
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
