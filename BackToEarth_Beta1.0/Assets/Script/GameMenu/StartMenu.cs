using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public static StartMenu _instance;
    private TweenAlpha tween;
	
    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenAlpha>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.SetInt("IsFirstOpen", 0);
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.SetInt("TotalEnergyPoint", 5);
        }
    }
    public void Show()
    {
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }

    public void OnStartGameClick()
    {
        Hide();
        SelectLevel._instance.Show();
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
