using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPropEventDialog : MonoBehaviour {
    public static GetPropEventDialog _instance;
    
    private List<Dialog> dialogList;
    public List<string> messageList;
    public List<float> showTime;
    private bool isTriggered = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Tina._instance.OnGetProp += this.OnGetProp;
    }

    private void OnDestroy()
    {
        Tina._instance.OnGetProp -= this.OnGetProp;
    }

    //获得道具时触发
    void OnGetProp()
    {
        if (isTriggered == false)
        {
            isTriggered = true;
            DialogManager._instance.dialogList = initDialog();
            DialogManager._instance.Show();
        }
    }

    private List<Dialog> initDialog()
    {
        dialogList = new List<Dialog>();
        for (int i = 0; i < messageList.Count; i++)
        {
            Dialog dialog = new Dialog();
            dialog.message = messageList[i];
            dialog.showtime = showTime[i];
            dialog.painting = "playerPainting";
            dialogList.Add(dialog);
        }
        return dialogList;
    }

}
