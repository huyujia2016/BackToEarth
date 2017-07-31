using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJewelPots : PropBase
{
    public string name = "攻击宝石";
    public string des = "这个宝石使你的攻击力+1";
    public string icon = "attackJewelPots";
    public PropType propType = PropType.AttackJewelPots;

    public override string Name
    {
        get
        {
            return name;
        }
    }

    public override string Des
    {
        get
        {
            return des;
        }
    }

    public override string Icon
    {
        get
        {
            return icon;
        }
    }

    public override PropType PropType
    {
        get
        {
            return propType;
        }
    }

    //获得宝石
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Tina._instance.GetProp(this))
            {
                DataSet.Instance().AttackAdditional += 1;
                SoundManager._instance.Play("getProp", this.transform.parent.gameObject.GetComponent<AudioSource>());
                GameManager._instance.Player.transform.Find("Anim/WeaponEffectPoint").gameObject.SetActive(true);
                MessageManager._instance.ShowMessage("攻击力+1");
                Destroy(this.gameObject);
            }
        }
    }

    public override void Use()
    {
        MessageManager._instance.ShowMessage("该道具已经生效！");
        SoundManager._instance.Play("drink", GameObject.Find("Pots").GetComponent<AudioSource>());
    }
}
