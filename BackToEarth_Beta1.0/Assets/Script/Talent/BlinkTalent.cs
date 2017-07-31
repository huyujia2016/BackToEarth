using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTalent : TalentBase
{
    public override void Use()
    {
        DataSet.Instance().isBlinkTalent = true;
    }

}
