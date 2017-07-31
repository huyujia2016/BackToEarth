using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropType
{
    HealthPots, EnergyPots,AttackJewelPots,PowerPots,c,d
}

public abstract class PropBase : MonoBehaviour
{

    public int StackNumber = 1;
    public abstract string Name { get; }
    public abstract string Des { get; }
    public abstract string Icon { get; }
    public abstract PropType PropType { get; }

    public virtual void Use()
    {
        PropBase prop = Tina._instance.PropList[Tina._instance.SelectedPropIndex];
        if (prop.PropType == this.PropType)
        {
            if (this.StackNumber == 1)
            {
                Tina._instance.PropList.Remove(prop);
            }
            else
            {
                prop.StackNumber--;
            }
            PropBar._instance.UpdatePropBar();
        }
    }
}