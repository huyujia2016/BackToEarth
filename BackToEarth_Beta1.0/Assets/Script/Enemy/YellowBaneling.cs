using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBaneling : EnemyBase {

	public override void Die()
    {
        base.Die();
        GameManager._instance.EnemyList.Remove(this.gameObject);
        GameManager._instance.PlayLayerList.Remove(this.gameObject);
        SoundManager._instance.Play("banelingDie", SoundManager._instance.GetComponent<AudioSource>());
        StartCoroutine(Dead());
    }

    //动画播放结束后处理
    IEnumerator Dead()
    {
        Destroy(HpBarGo);
        yield return new WaitForSeconds(0.5f);
        
        if (this.transform.parent!=null)
        {
            this.transform.parent.GetComponent<EnemyTrigger>().ActivationList.Remove(this.gameObject);
        }
        Destroy(HudTextGameObject);
        Destroy(this.gameObject);
    }
}
