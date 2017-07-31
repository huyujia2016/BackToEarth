using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentBase : MonoBehaviour {

    public List<UISprite> pathList;
    public List<TalentBase> TalentList;

    private UILabel desLabel;
    [HideInInspector]
    public UILabel levelLabel;

    public int MaxLevel;
    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public bool Enabled = false;
    //升级所需能量点数
    public int NeedEnergy;
    [HideInInspector]
    public UISprite LockSprite;

    private void Awake()
    {
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        LockSprite = transform.Find("LockSprite").GetComponent<UISprite>();
        levelLabel.text = currentLevel.ToString() + "/" + MaxLevel.ToString();
        GetComponent<UIButton>().state = UIButton.State.Disabled;
        GetComponent<BoxCollider>().enabled = false;
    }


    public void OnTalentClick()
    {
        if (enabled)
        {
            if (currentLevel < MaxLevel)
            {
                if (NeedEnergy <= DataSet.Instance().EnergyPoint)
                {
                    DataSet.Instance().EnergyPoint -= NeedEnergy;
                    currentLevel++;
                    levelLabel.text = currentLevel.ToString() + "/" + MaxLevel.ToString();
                    TalentMenu._instance.energyPoint.text = "剩余能量点：" + DataSet.Instance().EnergyPoint.ToString();
                }
            }
        }
    }

    public void SetEnable()
    {
        Enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<UIButton>().state = UIButton.State.Normal;
        this.GetComponent<UISprite>().color = Color.white;
        LockSprite.gameObject.SetActive(false);

        foreach (UISprite path in pathList)
        {
            path.color = Color.red;
        }
    }


    public abstract void Use();

    public void OnTalentHoverOn()
    {
        desLabel.gameObject.SetActive(true);
    }

    public void OnTalentHoverOut()
    {
        desLabel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isActivated())
        {
            SetEnable();
        }
    }


    //是否可以点击
    public bool isActivated()
    {
        bool flag = true;
        foreach (TalentBase talent in TalentList)
        {
            if (talent.currentLevel != talent.MaxLevel)
            {
                flag = false;
            }
        }
        return flag;
    }

}
