using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSet  {
    private static DataSet _instance = null;

    private DataSet() {
        InitDataSet();
    }

    public static DataSet Instance()
    {
        if (_instance == null)
        {
            _instance = new DataSet();
        }
        return _instance;
    }

    public void InitGame()
    {
        MoveSpeedAdditional = 0;
        AttackAdditional = AttackTalentLevel;
        SkillDamageAdditional = SkillDamageTalentLevel;
        HpAdditional = HpTalentLevel * 2;
        MpAdditional = MpTalentLevel * 2;
        isBlink = isBlinkTalent;
    }

    public void InitDataSet()
    {
        AttackTalentLevel = 0;
        SkillDamageTalentLevel = 0;
        HpTalentLevel = 0;
        MpTalentLevel = 0;
        isBlinkTalent = false;
        MoveSpeedAdditional = 0;
        if (PlayerPrefs.HasKey("IsFirstOpen"))
        {
            //isFirstOpen = true;
            //PlayerPrefs.SetInt("IsFirstOpen", 0);
            if (PlayerPrefs.GetInt("IsFirstOpen")==0)
            {
                isFirstOpen = true;
            }
            else
            {
                isFirstOpen = false;
            }
        }
        else
        {
            isFirstOpen = true;
            PlayerPrefs.SetInt("IsFirstOpen", 0);
        }
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            //CurrentLevel = 1;
            //PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            CurrentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }

        if (PlayerPrefs.HasKey("TotalEnergyPoint"))
        {
            //TotalEnergyPoint = 5;
            //PlayerPrefs.SetInt("TotalEnergyPoint", TotalEnergyPoint);
            TotalEnergyPoint = PlayerPrefs.GetInt("TotalEnergyPoint");
        }
        else
        {
            TotalEnergyPoint = 5;
            PlayerPrefs.SetInt("TotalEnergyPoint", TotalEnergyPoint);
        }
        EnergyPoint = TotalEnergyPoint;
    }

    public int HpTalentLevel = 0;
    public int MpTalentLevel = 0;
    public int AttackTalentLevel = 0;
    public int SkillDamageTalentLevel = 0;
    public bool isBlinkTalent = false;


    //当前关卡
    public int CurrentLevel=0;
    //总能量
    public int TotalEnergyPoint=0;
    //剩余能量
    public int EnergyPoint=0;

    //增加的最大生命
    public int HpAdditional;
    //增加的最大能量
    public int MpAdditional;
    //攻击力修正值
    public int AttackAdditional;
    //技能伤害修正值
    public int SkillDamageAdditional;
    //移动速度修正值
    public float MoveSpeedAdditional;
    //是否可以使用闪现技能
    public bool isBlink;
    public bool isFirstOpen;
}
