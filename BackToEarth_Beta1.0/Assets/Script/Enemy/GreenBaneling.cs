using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBaneling : EnemyBase {

    public GameObject Venom;

    public override void Die()
    {
        anim.SetTrigger("die");
        isActivated = false;
        SoundManager._instance.Play("venom", SoundManager._instance.GetComponent<AudioSource>());
        StartCoroutine(Dead());
        GameManager._instance.EnemyList.Remove(this.gameObject);
        GameManager._instance.PlayLayerList.Remove(this.gameObject);
    }

    //动画播放结束后处理
    IEnumerator Dead()
    {
        Destroy(HpBarGo);
        yield return new WaitForSeconds(0.2f);
        Instantiate(Venom, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        
        if (this.transform.parent!=null)
        {
            this.transform.parent.GetComponent<EnemyTrigger>().ActivationList.Remove(this.gameObject);
        }
        Destroy(HudTextGameObject);
        Destroy(this.gameObject);
    }

}
