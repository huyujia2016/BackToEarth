using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public string painting;
    public string message;
    public float showtime;

    //public delegate void OnDialogPlayedEvent();
    //public event OnDialogPlayedEvent OnDialogPlayed;
}

public class DialogManager : MonoBehaviour {

    public static DialogManager _instance;
    public List<Dialog> dialogList;

    private TweenPosition tween;
    private UILabel dialogLabel;
    private UISprite PaintingSprite;
    private float showTimer = 0;
    private float ShowTime;
    private bool isShow = false;
    //当前播放对话索引
    private int currentIndex = 0;

    private void Awake()
    {
        _instance = this;
        dialogList = new List<Dialog>();
        tween = GetComponent<TweenPosition>();
        dialogLabel = transform.Find("DialogLabel").GetComponent<UILabel>();
        PaintingSprite = transform.Find("PaintingSprite").GetComponent<UISprite>();
    }


    private void Update()
    {
        if (isShow)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                showTimer = ShowTime;
            }
            showTimer += Time.deltaTime;
            if (showTimer >= ShowTime)
            {
                PlayDialog();
            }
        }
        
    }

    private void PlayDialog()
    {
        showTimer = 0;
        if (dialogList.Count == currentIndex)
        {
            currentIndex = 0;
            Hide();
            return;
        }
        ShowTime = dialogList[currentIndex].showtime;
        dialogLabel.text = dialogList[currentIndex].message;
        PaintingSprite.spriteName = dialogList[currentIndex].painting;
        currentIndex++;
    }


    public void Show()
    {
        tween.PlayForward();
        PlayDialog();
        isShow = true;
    }

    private void Hide()
    {
        Tina._instance.isControlled = true;
        Tina._instance.isForceStoped = false;
        isShow = false;
        tween.PlayReverse();
    }
}
