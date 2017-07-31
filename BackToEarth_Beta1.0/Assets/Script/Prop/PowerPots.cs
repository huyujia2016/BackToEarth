using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPots : PropBase
{

    public string name = "力量药水";
    public string des = "10秒内伤害+5";
    public string icon = "PowerPots";
    public int addValue = 5;
    public float durationTime = 10;
    public PropType propType = PropType.PowerPots;

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
        if (PowerBuffUI._instance.UsePowerPot(addValue,durationTime))
        {
            base.Use();
            SoundManager._instance.Play("drink", GameObject.Find("Pots").GetComponent<AudioSource>());
        }
        
    }
}
