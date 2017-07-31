using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaBattle : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        if (VictoryMenu._instance.isVictory == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Menu._instance.Use();
            }
        }
        
        if (Tina._instance.isControlled)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                CommonAttack._instance.Use();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Whirlwind._instance.Use();
            }
            if (DataSet.Instance().isBlink)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Blink._instance.Use();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PropBar._instance.SelectProp(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PropBar._instance.SelectProp(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PropBar._instance.SelectProp(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PropBar._instance.SelectProp(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PropBar._instance.SelectProp(4);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                UseProp();
            }
        }
    }

    private void UseProp()
    {
        PropBar._instance.UpdatePropBar();
        if (Tina._instance.SelectedPropIndex == -1)
        {
            MessageManager._instance.ShowMessage("没有选中道具！");
            return;
        }
        if (Tina._instance.PropList.Count > Tina._instance.SelectedPropIndex)
        {
            Tina._instance.PropList[Tina._instance.SelectedPropIndex].Use();
        }
        else
        {
            MessageManager._instance.ShowMessage("该位置没有道具！");
        }
    }
}
