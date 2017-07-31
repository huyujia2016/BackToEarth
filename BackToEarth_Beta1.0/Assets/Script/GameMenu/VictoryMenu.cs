using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public static VictoryMenu _instance;
    private TweenAlpha tween;

    private UIButton QuitBtn;
    private UILabel GetEnergyLabel;
    [HideInInspector]
    public bool isVictory = false;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenAlpha>();
        QuitBtn = transform.Find("QuitBtn").GetComponent<UIButton>();
        GetEnergyLabel = transform.Find("GetEnergyLabel").GetComponent<UILabel>();
    }

    public void Show()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == DataSet.Instance().CurrentLevel)
        {
            GetEnergyLabel.text = "获得能量：" + GameManager._instance.RewardEnergy.ToString();
        }
        else
        {
            GetEnergyLabel.text = "重复过关无法获得奖励。" ;
        }
        Tina._instance.isControlled = false;
        isVictory = true;
        Tina._instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Tina._instance.Movedirection == MoveDirection.Right)
        {
            Tina._instance.anim.SetTrigger("right_idle");
        }
        else
        {
            Tina._instance.anim.SetTrigger("left_idle");
        }
        Time.timeScale = 0;
        tween.PlayForward();
    }

    public void OnQuitClick()
    {
        if (PlayerPrefs.GetInt("CurrentLevel")== DataSet.Instance().CurrentLevel)
        {
            int TotalEnergyPoint = DataSet.Instance().TotalEnergyPoint;
            TotalEnergyPoint += GameManager._instance.RewardEnergy;
            PlayerPrefs.SetInt("TotalEnergyPoint", TotalEnergyPoint);

            int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
            CurrentLevel += 1;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }

        DataSet.Instance().InitDataSet();
        SceneManager.LoadScene("MainMenu");
    }
}
