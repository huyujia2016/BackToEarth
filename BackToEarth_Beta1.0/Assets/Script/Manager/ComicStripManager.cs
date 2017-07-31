using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicStripManager : MonoBehaviour {

    public static ComicStripManager _instance;
    private List<ComicScene> ComicList;
    public int PlayIndex = 0;
    private bool isFirstStart = true;
    private float playTimer = 0;
    private float playTime = 99999;

    private void Awake()
    {
        DataSet.Instance().InitDataSet();
        _instance = this;
    }

    private void Update()
    {
        if (isFirstStart)
        {
            playTimer += Time.deltaTime;
            if (playTimer>= playTime)
            {
                Disappear();
            }
        }
    }


    private void Start()
    {
        if (DataSet.Instance().isFirstOpen)
        {
            ComicList = new List<ComicScene>();
            for (int i = 0; i < transform.childCount; i++)
            {
                ComicList.Add(transform.GetChild(i).GetComponent<ComicScene>());
            }
            Play(PlayIndex); 
        }
        else
        {
            isFirstStart = false;
            StartMenu._instance.Show();
        }
    }

    private void Play(int playIndex)
    {
        playTime = ComicList[PlayIndex].playTime;
        playTimer = 0;
        ComicList[playIndex].gameObject.SetActive(true);
        ComicList[playIndex].tween.PlayForward();
    }

    public void OnSkipCurrentSceneClick()
    {
        Disappear();
    }

    void Disappear()
    {
        ComicList[PlayIndex].tween.PlayReverse();
        ComicList[PlayIndex].gameObject.SetActive(false);
        if (PlayIndex < ComicList.Count - 1)
        {
            PlayIndex++;
            Play(PlayIndex);
        }
        else
        {
            isFirstStart = false;
            DataSet.Instance().isFirstOpen = false;
            PlayerPrefs.SetInt("IsFirstOpen", 1);
            StartMenu._instance.Show();
        }
    }
}
