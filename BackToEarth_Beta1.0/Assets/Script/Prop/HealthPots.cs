using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPots : PropBase {

    public string name = "血瓶";
    public string des = "生命药水：回复4点生命值";
    public string icon = "healthPots";
    public int addValue = 4;
    public PropType propType = PropType.HealthPots;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Tina._instance.GetProp(this))
            {
                SoundManager._instance.Play("getProp", this.transform.parent.gameObject.GetComponent<AudioSource>());
                Destroy(this.gameObject);
            }
        }
    }

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


    public override void Use()
    {
        base.Use();
        Tina._instance.ChangeHp(addValue);
        SoundManager._instance.Play("drink", GameObject.Find("Pots").GetComponent<AudioSource>());
    }
}
