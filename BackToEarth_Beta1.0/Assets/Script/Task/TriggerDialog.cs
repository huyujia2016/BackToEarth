using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour {

    private List<Dialog> dialogList;
    public List<string> messageList;
    public List<float> showTime;
    private bool isTriggered = false;
    public bool isStopTina = false;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isTriggered == false)
        {
            isTriggered = true;
            DialogManager._instance.dialogList = initDialog();
            if (isStopTina)
            {
                Tina._instance.Stop();
            }
            DialogManager._instance.Show();
        }
    }
}
