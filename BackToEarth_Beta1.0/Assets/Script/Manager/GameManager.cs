using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager _instance;
    [HideInInspector]
    public GameObject Player;
    public GameObject hudTextPrefab;
    public GameObject hpBarPrefab;
    [HideInInspector]
    public int RewardEnergy;

    [HideInInspector]
    public List<GameObject> EnemyList = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> PlayLayerList = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
        RewardEnergy = 5;
        Player = GameObject.FindGameObjectWithTag("Player");
        InitList();
        DataSet.Instance().InitGame();
    }

    private void Start()
    {
        Tina._instance.OnGetProp += this.OnGetProp;
    }

    private void OnGetProp()
    {
    }

    private void OnDestroy()
    {
        Tina._instance.OnGetProp -= this.OnGetProp;
    }

    private void InitList()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyList.Add(go);
            PlayLayerList.Add(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            EnemyList.Add(go);
            PlayLayerList.Add(go);
        }
        PlayLayerList.Add(Player);
    }

    public void AddEnemy(GameObject go)
    {
        EnemyList.Add(go);
        PlayLayerList.Add(go);
    }


    private void Update()
    {
        OrderPlayerLayer();
    }

    private void OrderPlayerLayer()
    {
        //按照y进行降序排序
        PlayLayerList.Sort(delegate (GameObject a, GameObject b)
        {
            return b.transform.position.y.CompareTo(a.transform.position.y);
        });

        //分配z-order
        for (int i = 0; i < PlayLayerList.Count; i++)
        {
            PlayLayerList[i].transform.Find("Anim").GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }

    //返回血条
    public GameObject GetHpBar(GameObject Target)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, hpBarPrefab);
        go.GetComponent<UIFollowTarget>().target = Target.transform;
        return go;
    }

    public GameObject GetHudText(GameObject target)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, hudTextPrefab);
        go.GetComponent<UIFollowTarget>().target = target.transform;
        return go;
    }
}
