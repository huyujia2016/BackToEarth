using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPots : PropBase {

    public string name = "能量瓶";
    public string des = "能量药水：回复5点能量值";
    public string icon = "energyPots";
    public int addValue = 5;
    public PropType propType = PropType.EnergyPots;

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
        Tina._instance.ChangeEnergy(addValue,true);
        SoundManager._instance.Play("drink", GameObject.Find("Pots").GetComponent<AudioSource>());
    }
}
