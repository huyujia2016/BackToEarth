using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttackTalent : TalentBase
{

    public override void Use()
    {
        DataSet.Instance().AttackTalentLevel += 1;
    }
}
