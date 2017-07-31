using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBar : MonoBehaviour {

    private TweenPosition tween;
    public static PropBar _instance;
    private float DisappearTime = 2;
    private float disappearTimer = 0;
    private bool disappeared = true;
    private UILabel DesLabel;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        DesLabel = transform.Find("DesLabel").GetComponent<UILabel>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!disappeared)
        {
            disappearTimer += Time.deltaTime;
            if (disappearTimer >= DisappearTime)
            {
                Hide();
            }
        }
	}

    private void Show()
    {
        SoundManager._instance.Play("showProp", this.GetComponent<AudioSource>());
        tween.PlayForward();
        if (Tina._instance.SelectedPropIndex == -1)
        {
            DesLabel.text = "";
        }
        else if (Tina._instance.PropList.Count > Tina._instance.SelectedPropIndex)
        {
            DesLabel.text = Tina._instance.PropList[Tina._instance.SelectedPropIndex].Des; 
        }
        else
        {
            DesLabel.text = "";
        }
        disappearTimer = 0;
        disappeared = false;
    }

    private void Hide()
    {
        tween.PlayReverse();
        disappeared = true;
    }

    public List<PropItem> PropItemList;

    public void UpdatePropBar()
    {
        for (int i = 0; i < Tina._instance.PropList.Count; i++)
        {
            PropItemList[i].SetProp(Tina._instance.PropList[i]);
        }
        for (int i = Tina._instance.PropList.Count; i < 5; i++)
        {
            PropItemList[i].Clear();
        }

        for (int i = 0; i < 5; i++)
        {
            PropItemList[i].GetComponent<UISprite>().color = Color.white;
        }
        if (Tina._instance.SelectedPropIndex != -1)
        {
            PropItemList[Tina._instance.SelectedPropIndex].GetComponent<UISprite>().color = Color.red;
        }
        Show();
    }

    public void SelectProp(int index)
    {
        Tina._instance.SelectedPropIndex = index;
        for (int i = 0; i < 5; i++)
        {
            PropItemList[i].GetComponent<UISprite>().color = Color.white;
        }
        if (index != -1)
        {
            PropItemList[index].GetComponent<UISprite>().color = Color.red;
        }
        Show();
    }
}
