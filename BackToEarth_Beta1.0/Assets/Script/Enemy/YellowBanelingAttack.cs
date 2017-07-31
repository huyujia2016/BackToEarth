using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBanelingAttack : MonoBehaviour {

    public void Attack()
    {
        SoundManager._instance.Play("banelingAttack", this.transform.parent.GetComponent<AudioSource>());
        if (this.transform.parent.GetComponent<YellowBaneling>().hp<=0)
        {
            return;
        }
        Vector2 pos = this.transform.InverseTransformPoint(GameManager._instance.Player.transform.position);
        if (this.transform.parent.GetComponent<YellowBaneling>().direction == MoveDirection.Left)
        {
            if (pos.x > -0.5f && pos.x < 0.1f && pos.y > -1.4f && pos.y < 0.3f)
            {
                Tina._instance.SendMessage("TakeDamage", this.transform.parent.GetComponent<YellowBaneling>().damage, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if (pos.x > -0.1f && pos.x < 0.5f && pos.y > -1.4f && pos.y < 0.3f)
            {
                Tina._instance.SendMessage("TakeDamage", this.transform.parent.GetComponent<YellowBaneling>().damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
