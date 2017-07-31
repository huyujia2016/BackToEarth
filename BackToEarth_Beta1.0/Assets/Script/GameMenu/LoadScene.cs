using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public static LoadScene _instace;
    private UISlider ProgressBar;
    private GameObject bg;
    private bool isAsyn = false;
    private AsyncOperation ao = null;
    private UILabel ProgressLabel;
    private void Awake()
    {
        _instace = this;
        ProgressBar = transform.Find("Bg/ProgressBar").GetComponent<UISlider>();
        ProgressLabel= transform.Find("Bg/ProgressBar/Label").GetComponent<UILabel>();
        bg = transform.Find("Bg").gameObject;
        gameObject.SetActive(false);

    }

    public void Show()
    {
        gameObject.SetActive(true);
        bg.SetActive(true);
        isAsyn = true;
        StartCoroutine(loadScene());
    }
    private IEnumerator loadScene()
    {
        //异步读取场景。
        ao = SceneManager.LoadSceneAsync("Level0" + DataSet.Instance().CurrentLevel.ToString());
        yield return ao;
    }
    private void Update()
    {
        if (isAsyn)
        {
            ProgressBar.value = ao.progress;
            ProgressLabel.text = (ao.progress * 100).ToString("f2") +"%";
        }
    }
}
