using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaMove : MonoBehaviour {


    private Rigidbody2D rb;
    private Animator anim;
    private float moveSpeed;
    private Tina tina;

    //击退效果参数
    private bool isSpecialMove = false;
    private float specialMoveTime = 1f;
    private float specialMoveTimer = 0f;
    private Vector2 specialMoveDirection;
    private float specialMoveSpeed = 10;

    // Use this for initialization
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.Find("Anim").GetComponent<Animator>();
    }

    private void Start()
    {
        tina = Tina._instance;
        moveSpeed = tina.MoveSpeed;
    }

    //0.伤害值 
    //1.攻击对象名
    //2.击退速度
    //3.击退时间
    public void TakeDamageBeatBack(string args)
    {
        int hp = Tina._instance.CurrentHp;
        if (hp <= 0)
        {
            return;
        }
        string[] proArray = args.Split(',');

        //减去伤害值
        int damage = int.Parse(proArray[0]);
        Tina._instance.ChangeHp(-damage);
        hp = Tina._instance.CurrentHp;
        SoundManager._instance.Play("hurt", this.transform.Find("Anim").GetComponent<AudioSource>());

        //后退
        if (float.Parse(proArray[2]) > 0)
        {
            BeatBack(GameObject.Find(proArray[1]), float.Parse(proArray[2]), float.Parse(proArray[3]));
        }
        if (hp <= 0)
        {
            Tina._instance.Die();
        }
    }

    void BeatBack(GameObject enemy, float velocity, float time)
    {
        specialMoveDirection = GameManager._instance.Player.transform.position - enemy.transform.position;
        specialMoveDirection.Normalize();
        specialMoveTime = time;
        specialMoveSpeed = velocity;
        Tina._instance.isControlled = false;
        isSpecialMove = true;
    }

    // Update is called once per frame
    void Update () {
        if (Tina._instance.isForceStoped == false)
        {
            if (isSpecialMove)
            {
                specialMoveTimer += Time.deltaTime;
                if (specialMoveTimer >= specialMoveTime)
                {
                    isSpecialMove = false;
                    Tina._instance.isControlled = true;
                    specialMoveTimer = 0;
                }
                else
                {
                    rb.velocity = new Vector2(specialMoveDirection.x * specialMoveSpeed, specialMoveDirection.y * specialMoveSpeed);
                }
            }

            if (Tina._instance.isControlled)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
                {
                    if (h == 0)
                    {
                        if (tina.Movedirection == MoveDirection.Right)
                        {
                            anim.SetTrigger("right_run");
                        }
                        else
                        {
                            anim.SetTrigger("left_run");
                        }
                    }
                    else if (h > 0)
                    {
                        tina.Movedirection = MoveDirection.Right;
                        anim.SetTrigger("right_run");
                    }
                    else
                    {
                        tina.Movedirection = MoveDirection.Left;
                        anim.SetTrigger("left_run");
                    }

                    rb.velocity = new Vector2(h * (moveSpeed + DataSet.Instance().MoveSpeedAdditional), v * (moveSpeed + DataSet.Instance().MoveSpeedAdditional));
                    SoundManager._instance.Play("run", this.GetComponent<AudioSource>(), false);
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    SoundManager._instance.StopPlay(this.GetComponent<AudioSource>());
                    if (tina.Movedirection == MoveDirection.Right)
                    {
                        anim.SetTrigger("right_idle");
                    }
                    else
                    {
                        anim.SetTrigger("left_idle");
                    }
                }
            }
        }
    }
}
