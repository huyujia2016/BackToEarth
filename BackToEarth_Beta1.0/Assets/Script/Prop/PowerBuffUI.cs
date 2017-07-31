using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuffUI : MonoBehaviour {

    public static PowerBuffUI _instance;
    private UILabel label;
    private UISlider powerBuffSlider;
    private float durationTime;
    private float _durationTimer;
    private int addValue;
    private bool isUse = false;
    private TweenPosition tween;

    private void Awake()
    {
        _instance = this;
        powerBuffSlider = this.GetComponent<UISlider>();
        label = transform.Find("Label").GetComponent<UILabel>();
        tween = GetComponent<TweenPosition>();
    }
    

	public bool UsePowerPot(int AddValue,float DurationTime)
    {
        if (isUse==false)
        {
            DataSet.Instance().AttackAdditional += AddValue;
            DataSet.Instance().SkillDamageAdditional += AddValue;
            MessageManager._instance.ShowMessage("伤害+5");

            durationTime = DurationTime;
            addValue = AddValue;
            isUse = true;
            tween.PlayForward();
            return true;
        }
        else
        {
            MessageManager._instance.ShowMessage("你已经使用了力量药剂！");
            return false;
        }
        
    }

	void Update () {
        if (isUse)
        {
            _durationTimer += Time.deltaTime;
            powerBuffSlider.value = _durationTimer / durationTime;
            label.text = (durationTime - _durationTimer).ToString("F1");
            if (_durationTimer>= durationTime)
            {
                _durationTimer = 0;
                DataSet.Instance().AttackAdditional -= addValue;
                DataSet.Instance().SkillDamageAdditional -= addValue;
                isUse = false;
                tween.PlayReverse();
            }
        }
	}
}
