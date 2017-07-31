using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLineSwitchManager : MonoBehaviour {

    public static EnergyLineSwitchManager _instance;
    public List<EnergyLineTrap> TrapLineList;
    public List<EnergyLineSwitch> SwitchList;

    private EnergyLineType ActivedType1;
    private EnergyLineType ActivedType2;

    void Awake()
    {
        _instance = this;
        ActivedType1 = EnergyLineType.NotActived;
        ActivedType2 = EnergyLineType.NotActived;
    }

    public void ActiveTrap(EnergyLineType type)
    {
        foreach (EnergyLineTrap _trap in TrapLineList)
        {
            if (_trap.Type == type)
            {
                _trap.SetActive();
            }
        }
        foreach (EnergyLineSwitch _switch in SwitchList)
        {
            if (_switch.Type == type)
            {
                _switch.CloseSwitch();
            }
        }
    }

    public void DisActiveTrap(EnergyLineType type)
    {
        if (ActivedType1== EnergyLineType.NotActived)
        {
            ActivedType1 = type;
        }
        else if (ActivedType2 == EnergyLineType.NotActived)
        {
            ActivedType2 = type;
        }
        else
        {
            ActiveTrap(ActivedType1);
            ActivedType1 = ActivedType2;
            ActivedType2 = type;
        }
        EnergyLineUI._instance.ChangeActivedLabel(ActivedType1, ActivedType2);
        foreach (EnergyLineTrap _trap in TrapLineList)
        {
            if (_trap.Type == type)
            {
                _trap.SetDisActive();
            }
        }
        foreach (EnergyLineSwitch _switch in SwitchList)
        {
            if (_switch.Type == type)
            {
                _switch.OpenSwitch();
            }
        }
    }
}
