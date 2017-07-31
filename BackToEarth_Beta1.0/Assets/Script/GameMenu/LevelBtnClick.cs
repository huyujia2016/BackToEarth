using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBtnClick : MonoBehaviour {

    public int currentLevel;
    public void OnClick()
    {
        TalentMenu._instance.OnResetBtnClick();
        TalentMenu._instance.Show(currentLevel);
        SelectLevel._instance.Hide();
    }
}
