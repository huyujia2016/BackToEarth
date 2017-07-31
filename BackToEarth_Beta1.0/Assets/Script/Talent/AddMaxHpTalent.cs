using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//本层天赋达到三级开启下一层天赋
public class AddMaxHpTalent : TalentBase {

    public override void Use()
    {
        DataSet.Instance().HpTalentLevel ++;
    }
}
