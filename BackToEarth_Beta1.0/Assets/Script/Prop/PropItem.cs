using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropItem : MonoBehaviour {

    private UISprite PropIconSprite;
    private UILabel StackNumberLabel;
    [HideInInspector]
    public PropBase prop;
    private bool Selected =false;

    private void Awake()
    {
        PropIconSprite = transform.Find("Sprite").GetComponent<UISprite>();
        StackNumberLabel = transform.Find("Label").GetComponent<UILabel>();

    }

    public void SetProp(PropBase prop)
    {
        this.prop = prop;
        PropIconSprite.spriteName = prop.Icon;
        StackNumberLabel.text = prop.StackNumber.ToString();
    }

    public void Clear()
    {
        prop = null;
        StackNumberLabel.text = "";
        PropIconSprite.spriteName = "PropUIBg";
    }


    public void ChangeCount(int count)
    {

        if ((prop.StackNumber + count) <= 0)
        {
            Clear();
        }
        else if ((prop.StackNumber + count) == 1)
        {
            StackNumberLabel.text = "";
        }
        else
        {
            StackNumberLabel.text = (prop.StackNumber + count).ToString();
        }
    }


}
