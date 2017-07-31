using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public static MessageManager _instance;
    private UILabel MessageLabel;
    private TweenAlpha tween;
    private bool isSetActice = false;

    void Awake()
    {
        _instance = this;
        MessageLabel = this.GetComponent<UILabel>();
        tween = this.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnTweenFinished");
        tween.onFinished.Add(ed);

        gameObject.SetActive(false);
    }

    public void ShowMessage(string message, float time = 1)
    {
        gameObject.SetActive(true);
        isSetActice = true;
        tween.PlayForward();
        MessageLabel.text = message;
        StartCoroutine(DisappearMessage(message, time));
    }

    IEnumerator DisappearMessage(string message, float time)
    {
        yield return new WaitForSeconds(time);
        tween.PlayReverse();
    }

    public void OnTweenFinished()
    {
        if (!isSetActice)
        {
            gameObject.SetActive(false);
        }
    }
}
