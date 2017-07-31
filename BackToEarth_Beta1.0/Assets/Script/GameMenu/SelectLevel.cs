using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour {

    public List<GameObject> LevelBtn;
    private TweenPosition tween;
    public static SelectLevel _instance;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    public void Show()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");;
        for (int i = 0; i < currentLevel; i++)
        {
            LevelBtn[i].GetComponent<UIButton>().state = UIButton.State.Normal;
            LevelBtn[i].GetComponent<BoxCollider>().enabled = true;
            LevelBtn[i].transform.Find("Label").GetComponent<UILabel>().color = Color.white;
        }
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }

    public void OnBackBtnClick()
    {
        Hide();
        StartMenu._instance.Show();
    }
}
