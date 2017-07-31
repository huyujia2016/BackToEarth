using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBanelingNestAttack : MonoBehaviour {

    public void MeleeAttack()
    {
        SoundManager._instance.Play(this.transform.parent.GetComponent<CrystalBanelingNest>().attackAudioName, this.transform.parent.GetComponent<AudioSource>());
        if (this.transform.parent.GetComponent<CrystalBanelingNest>().hp <= 0)
        {
            return;
        }
        Vector2 pos = this.transform.parent.transform.InverseTransformPoint(GameManager._instance.Player.transform.position);
        //BOSS朝向左边攻击
        if (this.transform.parent.GetComponent<CrystalBanelingNest>().direction==MoveDirection.Left)
        {
            if (pos.x > -1.7f && pos.x < 0.65f && pos.y > -1.7f && pos.y < 2.5f)
            {
                Tina._instance.SendMessage("TakeDamageBeatBack", this.transform.parent.GetComponent<CrystalBanelingNest>().MeleeDamage + ",CrystalBanelingNest,15,0.2", SendMessageOptions.DontRequireReceiver);
            }
        }
        //BOSS朝向右边攻击
        else
        {
            if (pos.x > -0.2f && pos.x < 2f && pos.y > -1.7f && pos.y < 2.5f)
            {
                Tina._instance.SendMessage("TakeDamageBeatBack", this.transform.parent.GetComponent<CrystalBanelingNest>().MeleeDamage + ",CrystalBanelingNest,15,0.2", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
