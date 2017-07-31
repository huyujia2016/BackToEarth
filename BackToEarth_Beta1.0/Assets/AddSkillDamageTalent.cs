using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkillDamageTalent : TalentBase
{
    public override void Use()
    {
        DataSet.Instance().SkillDamageTalentLevel += 1;
    }
}
