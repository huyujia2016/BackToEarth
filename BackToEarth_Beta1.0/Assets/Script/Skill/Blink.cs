using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : SkillBase {

    public static Blink _instance;
    public float cooldown;
    public int energycost;

    private Tina tina;
    private Animator anim;
    private GameObject Player;
    public float BlinkDistance;

    public void Start()
    {
        anim = this.GetComponent<Animator>();
        tina = Tina._instance;
        Player = GameManager._instance.Player;
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
                //播放动画
                //TODO
                Vector2 pos = GetBlinkPostion();
                Player.transform.position = pos;
                base.Use();
            }
            else
            {
                //技能正在冷却
                MessageManager._instance.ShowMessage("技能还在冷却！");
            }
        }
        else
        {
            //能量不足
            MessageManager._instance.ShowMessage("能量不足！");
        }
    }

    private Vector2 GetBlinkPostion()
    {
        float blinkDistance = BlinkDistance;
        while (blinkDistance >= 0.1f)
        {
            Vector2 BlinkPostion = Player.transform.position;
            switch (tina.Movedirection)
            {
                case MoveDirection.Right:
                    BlinkPostion.x += blinkDistance;
                    if (isHit(BlinkPostion))
                    {
                        blinkDistance -= 0.1f;
                    }
                    else
                    {
                        return BlinkPostion;
                    }
                    break;
                case MoveDirection.Left:
                    BlinkPostion.x -= blinkDistance;
                    if (isHit(BlinkPostion))
                    {
                        blinkDistance -= 0.1f;
                    }
                    else
                    {
                        return BlinkPostion;
                    }
                    break;
                default:
                    break;
            }
        }
        return Player.transform.position;
        //float blinkDistance = 0.2f;
        //while (blinkDistance < BlinkDistance)
        //{
        //    Vector2 BlinkPostion = Player.transform.position;
        //    switch (tina.Movedirection)
        //    {
        //        case MoveDirection.Right:
        //            BlinkPostion.x += blinkDistance;
        //            if (isHit(BlinkPostion))
        //            {
        //                BlinkPostion.x -= 0.2f;
        //                return BlinkPostion;
        //            }
        //            else
        //            {
        //                blinkDistance += 0.2f;
        //                continue;
        //            }
        //        case MoveDirection.Left:
        //            BlinkPostion.x -= blinkDistance;
        //            if (isHit(BlinkPostion))
        //            {
        //                BlinkPostion.x += 0.2f;
        //                return BlinkPostion;
        //            }
        //            else
        //            {
        //                blinkDistance += 0.2f;
        //                continue;
        //            }
        //        default:
        //            break;
        //    }
        //}
        //Vector2 pos = Player.transform.position;
        //switch (tina.Movedirection)
        //{
        //    case MoveDirection.Right:
        //        pos.x += blinkDistance;
        //        break;
        //    case MoveDirection.Left:
        //        pos.x -= blinkDistance;
        //        break;
        //    default:
        //        break;
        //}
        //return pos;

    }

    public bool isHit(Vector2 BlinkPostion)
    {
        bool ishit = false;
        CapsuleCollider2D playerCollider = Player.GetComponent<CapsuleCollider2D>();
        ishit = Physics2D.Linecast(new Vector2(BlinkPostion.x + playerCollider.size.x / 2, BlinkPostion.y + playerCollider.size.y / 2)
            , new Vector2(BlinkPostion.x + playerCollider.size.x / 2, BlinkPostion.y - playerCollider.size.y / 2)
            , 1 << LayerMask.NameToLayer("Wall"));
        if (ishit==false)
        {
            ishit = Physics2D.Linecast(new Vector2(BlinkPostion.x + playerCollider.size.x / 2, BlinkPostion.y - playerCollider.size.y / 2)
            , new Vector2(BlinkPostion.x + playerCollider.size.x / 2, BlinkPostion.y + playerCollider.size.y / 2)
            ,1 << LayerMask.NameToLayer("Obstacle"));
        }
        return ishit;
    }
}
