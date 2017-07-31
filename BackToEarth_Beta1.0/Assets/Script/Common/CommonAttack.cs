using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttack : SkillBase
{
    public static CommonAttack _instance;
    public float cooldown;
    public int energycost;
    public int damage;

    private Tina tina;
    private Animator anim;

    public void Start()
    {
        anim = this.GetComponent<Animator>();
        tina = Tina._instance;
        _instance = this;
    }

    public override float CoolDown
    {
        get
        {
            return cooldown;
        }
    }

    public override float EnergyCost
    {
        get
        {
            return energycost;
        }
    }

    public override void Use()
    {
        if (tina.CurrentEnergy >= energycost)
        {
            if (IsNotInCoolDown)
            {
                tina.ChangeEnergy(-energycost);
                SoundManager._instance.Play("hei01", this.GetComponent<AudioSource>());
                if (tina.Movedirection == MoveDirection.Left)
                {
                   anim.SetTrigger("left_attack");
                }
                else
                {
                   anim.SetTrigger("right_attack");
                }
                base.Use();
            }
        }
        else
        {
            //能量不足
            MessageManager._instance.ShowMessage("能量不足！");
        }
    }


    //0 攻击范围：0为前方，1为周围
    //1音效
    public void CommonAttackEvent(string args)
    {
        SoundManager._instance.Play("commonAttack", this.GetComponent<AudioSource>());
        string[] proArray = args.Split(',');
        List<GameObject> enemyList;
        if (int.Parse(proArray[0]) == 0)
        {
            enemyList = GetEnemyInAttackRange(AttackRange.Forward);
        }
        else
        {
            enemyList = GetEnemyInAttackRange(AttackRange.Around);
        }
        foreach (GameObject go in enemyList)
        {
            go.SendMessage("TakeDamage", damage + DataSet.Instance().AttackAdditional, SendMessageOptions.DontRequireReceiver);
        }
    }
}

