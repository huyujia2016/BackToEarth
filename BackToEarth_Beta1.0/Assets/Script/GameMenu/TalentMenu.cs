using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalentMenu : MonoBehaviour {

    private TweenAlpha tween;
    public static TalentMenu _instance;
    [HideInInspector]
    public UILabel currentLevelLabel;
    [HideInInspector]
    public UILabel energyPoint;
    public List<TalentBase> talentList;
    public List<UISprite> pathList;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenAlpha>();
        currentLevelLabel = transform.Find("CurrentLevel").GetComponent<UILabel>();
        energyPoint = transform.Find("EnergyPoint").GetComponent<UILabel>();
    }

    public void Show(int currentLevel)
    {
        DataSet.Instance().CurrentLevel = currentLevel;
        currentLevelLabel.text = "当前关卡：" + currentLevel.ToString();
        energyPoint.text = "剩余能量点：" + DataSet.Instance().EnergyPoint.ToString();
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }

    public void OnBackToSelectLevelBtnClick()
    {
        Hide();
        SelectLevel._instance.Show();
    }

    public void OnGameStartBtnClick()
    {
        foreach (TalentBase talent in talentList)
        {
            if (talent.currentLevel >= 0)
            {
                for (int i = 0; i < talent.currentLevel; i++)
                {
                    talent.Use();
                }
            }
        }
        LoadScene._instace.Show();
       // SceneManager.LoadScene("Level0" + CurrentLevel.ToString());
    }

    

    public void OnResetBtnClick()
    {
        foreach (TalentBase talent in talentList)
        {
            talent.Enabled = false;
            talent.GetComponent<UIButton>().state= UIButton.State.Disabled;
            talent.GetComponent<BoxCollider>().enabled = false;
            talent.LockSprite.gameObject.SetActive(true);
            talent.currentLevel = 0;
            talent.levelLabel.text = "0/" + talent.MaxLevel.ToString();
        }

        foreach (UISprite path in pathList)
        {
            path.color = Color.gray;
        }

        DataSet.Instance().InitDataSet();
        energyPoint.text = "剩余能量点：" + DataSet.Instance().EnergyPoint.ToString();
    }
}
