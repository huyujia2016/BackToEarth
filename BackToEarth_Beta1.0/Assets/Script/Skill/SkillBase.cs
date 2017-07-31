using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackRange
{
    Forward, Around
}

public abstract class SkillBase : MonoBehaviour
{

    public abstract float CoolDown { get; }
    public abstract float EnergyCost { get; }
    private float coolDownTimer = 0;//剩余冷却时间计时器
    //技能是否在冷却
    public bool IsNotInCoolDown = true;


    // Update is called once per frame
    public virtual void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            IsNotInCoolDown = true;
        }
    }

    public virtual void Use()
    {
        coolDownTimer = CoolDown;
        IsNotInCoolDown = false;
    }

    

    //得到攻击范围内的敌人
    public List<GameObject> GetEnemyInAttackRange(AttackRange attackRange)
    {
        List<GameObject> EnemyList = new List<GameObject>();
        Tina tina = Tina._instance;

        if (attackRange == AttackRange.Forward)
        {
            switch (tina.Movedirection)
            {
                case MoveDirection.Right:
                    foreach (GameObject go in GameManager._instance.EnemyList)
                    {
                        //将go的世界坐标转化成主角的局部坐标
                        Vector2 pos = GameManager._instance.Player.transform.InverseTransformPoint(go.transform.position);
                        if (pos.x >= 0)
                        {
                            float distance = Vector2.Distance(GameManager._instance.Player.transform.position, go.transform.position);
                            if (go.GetComponent<CapsuleCollider2D>()!=null)
                            {
                                distance -= (go.GetComponent<CapsuleCollider2D>().size.x * go.transform.lossyScale.x / 2 + GameManager._instance.Player.GetComponent<CapsuleCollider2D>().size.x / 2);
                            }

                            if (distance < 0.5f)
                            {
                                EnemyList.Add(go);
                            }
                        }
                        

                        //if (pos.x  < 1f && pos.x > -0.1f && pos.y < 1 && pos.y > -0.3f)
                        //{
                        //    EnemyList.Add(go);
                        //}
                    }
                    break;
                case MoveDirection.Left:
                    foreach (GameObject go in GameManager._instance.EnemyList)
                    {
                        //将go的世界坐标转化成主角的局部坐标
                        Vector2 pos = GameManager._instance.Player.transform.InverseTransformPoint(go.transform.position);
                        if (pos.x <= 0)
                        {
                            float distance = Vector2.Distance(GameManager._instance.Player.transform.position, go.transform.position);
                            if (go.GetComponent<CapsuleCollider2D>() != null)
                            {
                                distance -= (go.GetComponent<CapsuleCollider2D>().size.x * go.transform.lossyScale.x / 2 + GameManager._instance.Player.GetComponent<CapsuleCollider2D>().size.x / 2);
                            }
                            if (distance < 0.5f)
                            {
                                EnemyList.Add(go);
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            foreach (GameObject go in GameManager._instance.EnemyList)
            {
                //Vector2 pos = GameManager._instance.Player.transform.InverseTransformPoint(go.transform.position);
                float distance = Vector2.Distance(GameManager._instance.Player.transform.position, go.transform.position);
                if (go.GetComponent<CapsuleCollider2D>() != null)
                {
                    distance -= (go.GetComponent<CapsuleCollider2D>().size.x * go.transform.lossyScale.x / 2 + GameManager._instance.Player.GetComponent<CapsuleCollider2D>().size.x / 2);
                }
                if (distance < 1.5f)
                {
                    EnemyList.Add(go);
                }
                //if (pos.x  > -1f && pos.x  < 1f && pos.y < 1 && pos.y > -0.3f)
                //{
                //    EnemyList.Add(go);
                //}
            }
        }
        return EnemyList;
    }

}
