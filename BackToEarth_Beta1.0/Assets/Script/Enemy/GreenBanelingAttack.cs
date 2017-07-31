using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBanelingAttack : MonoBehaviour {

    public void Attack()
    {
        SoundManager._instance.Play("banelingAttack", this.transform.parent.GetComponent<AudioSource>());
        if (this.transform.parent.GetComponent<GreenBaneling>().hp <= 0)
        {
            return;
        }
        Vector2 pos = this.transform.InverseTransformPoint(GameManager._instance.Player.transform.position);
        if (pos.x > -1.2f && pos.x < 1f && pos.y > -1.2f && pos.y < 0.6f)
        {
            Tina._instance.SendMessage("TakeDamage", this.transform.parent.GetComponent<GreenBaneling>().damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
