using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Right, Left
}


public class Tina : MonoBehaviour {

    public int MaxEnergy;
    [HideInInspector]
    public int CurrentEnergy;
    public int MaxHp;
    [HideInInspector]
    public int CurrentHp;
    public int EnergyRecovery;
    public float MoveSpeed;
    public MoveDirection Movedirection;
    [HideInInspector]
    public bool isControlled = true;
    [HideInInspector]
    public bool isForceStoped = false;

    private GameObject HudTextGameObject;
    private HUDText hudText;

    public delegate void OnTinaInfoChangedEvent();
    public event OnTinaInfoChangedEvent OnTinaInfoChanged;
    public delegate void OnGetPropEvent();
    public event OnGetPropEvent OnGetProp;
    [HideInInspector]
    public Animator anim;

    public static Tina _instance;
    private void Awake()
    {
        _instance = this;
        LoadGame();
        this.CurrentEnergy = MaxEnergy;
        this.CurrentHp = MaxHp;
        this.Movedirection = MoveDirection.Left;
        HudTextGameObject = GameManager._instance.GetHudText(transform.Find("HpPoint").gameObject);
        hudText = HudTextGameObject.GetComponent<HUDText>();
        anim = transform.Find("Anim").GetComponent<Animator>();
        SelectedPropIndex = -1;
    }

    private float energyTimer = 0;

    private void Update()
    {
        //每秒回能
        if (CurrentEnergy < MaxEnergy)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer > 1)
            {
                ChangeEnergy(EnergyRecovery);
                energyTimer -= 1;
            }
        }
    }


    //加载存档TODO
    private void LoadGame()
    {
        DataSet.Instance().InitGame();
        MaxEnergy += DataSet.Instance().MpAdditional;
        MaxHp += DataSet.Instance().HpAdditional;
        Time.timeScale = 1;
    }

    //能量值变化
    public void ChangeEnergy(int value, bool showText = false)
    {
        if (CurrentEnergy + value > MaxEnergy)
        {
            value = MaxEnergy - CurrentEnergy;
        }
        CurrentEnergy += value;
        if (showText)
        {
            if (value >= 0)
            {
                hudText.Add("+" + value, Color.blue, 0.3f);
            }
            else
            {
                hudText.Add(value, Color.blue, 0.3f);
            }
        }
        OnTinaInfoChanged();
    }

    //血量变化
    public void ChangeHp(int value)
    {
        if (CurrentHp + value > MaxHp)
        {
            value = MaxHp - CurrentHp;
        }
        if (CurrentHp + value < 0)
        {
            value = -CurrentHp;
        }
        CurrentHp += value;

        if (value >= 0)
        {
            hudText.Add("+" + value, Color.green, 0.3f);
        }
        else
        {
            hudText.Add(value, Color.red, 0.3f);
        }
        OnTinaInfoChanged();
    }

    void TakeDamage(int damage)
    {
        float random = Random.Range(0, 1f);
        if (random < 0.5f)
        {
            SoundManager._instance.Play("hurt", this.transform.Find("Anim").GetComponent<AudioSource>());
        }

        if (CurrentHp <= 0)
        {
            return;
        }

        ChangeHp(-damage);
        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    public void Stop()
    {
        isControlled = false;
        isForceStoped = true;
        if (this.Movedirection == MoveDirection.Right)
        {
            anim.SetTrigger("right_idle");
        }
        else
        {
            anim.SetTrigger("left_idle");
        }
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }


    public void Die()
    {
        SoundManager._instance.Play("die", this.transform.Find("Anim").GetComponent<AudioSource>());
        isControlled = false;
        isForceStoped = true;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (this.Movedirection == MoveDirection.Right)
        {
            anim.SetTrigger("right_die");
        }
        else
        {
            anim.SetTrigger("left_die");
        }
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        Menu._instance.Show();
    }


    public List<PropBase> PropList;
    [HideInInspector]
    public int SelectedPropIndex=-1;

    public bool GetProp(PropBase prop)
    {
        OnGetProp();
        foreach (PropBase p in PropList)
        {
            if (prop.PropType == p.PropType)
            {
                p.StackNumber++;
                PropBar._instance.UpdatePropBar();
                return true;
            }
        }
        if (PropList.Count == 5)
        {
            MessageManager._instance.ShowMessage("道具已满！");
            return false;
        }
        else
        {
            PropList.Add(prop);
            PropBar._instance.UpdatePropBar();
            return true;
        }
    }

}
