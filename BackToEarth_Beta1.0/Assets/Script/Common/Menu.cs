using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public static Menu _instance;
    private TweenAlpha tween;

    private bool isRestart;
    private UIButton RestartBtn;
    private UIButton QuitBtn;
    private UILabel RestartLabel;
    private UILabel QuitLabel;
    private UILabel DefeatLabel;
    private bool isMenuShow = false;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenAlpha>();
        RestartBtn = transform.Find("RestartBtn").GetComponent<UIButton>();
        QuitBtn = transform.Find("QuitBtn").GetComponent<UIButton>();
        RestartLabel = transform.Find("RestartBtn").GetComponent<UILabel>();
        QuitLabel = transform.Find("QuitBtn").GetComponent<UILabel>();
        DefeatLabel = transform.Find("DefeatLabel").GetComponent<UILabel>();
        RestartLabel.color = Color.white;
        QuitLabel.color = Color.gray;
        isRestart = true;
        DefeatLabel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isMenuShow)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!isRestart)
                {
                    isRestart = true;
                    RestartLabel.color = Color.white;
                    QuitLabel.color = Color.gray;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (isRestart)
                {
                    isRestart = false;
                    QuitLabel.color = Color.white;
                    RestartLabel.color = Color.gray;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isRestart)
                {
                    OnRestartClick();
                }
                else
                {
                    OnQuitClick();
                }
            }
        }
    }

    //按ESC显示或者退出菜单
    public void Use()
    {

        if (isMenuShow)
        {
            //角色死亡后不显示菜单
            if (Tina._instance.CurrentHp<=0)
            {
                return;
            }
            Hide();
        }
        else
        {
            Show();
        }
    }
    
    public void Show()
    {
        if (Tina._instance.CurrentHp <= 0)
        {
            DefeatLabel.gameObject.SetActive(true);
        }
        Tina._instance.isControlled = false;
        isMenuShow = true;
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

    public void Hide()
    {
        tween.PlayReverse();
        Time.timeScale = 1;
        Tina._instance.isControlled = true;
        isMenuShow = false;
        RestartLabel.color = Color.white;
        QuitLabel.color = Color.gray;
        isRestart = true;
        if (Tina._instance.Movedirection == MoveDirection.Right)
        {
            Tina._instance.anim.SetTrigger("right_idle");
        }
        else
        {
            Tina._instance.anim.SetTrigger("left_idle");
        }
    }

    public void OnRestartClick()
    {
        Hide();
        DataSet.Instance().InitGame();
        SceneManager.LoadScene(Application.loadedLevelName);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
