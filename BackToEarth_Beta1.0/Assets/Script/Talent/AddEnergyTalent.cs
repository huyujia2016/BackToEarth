using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnergyTalent : TalentBase
{

    public override void Use()
    {
        DataSet.Instance().MpTalentLevel++;
    }
}
