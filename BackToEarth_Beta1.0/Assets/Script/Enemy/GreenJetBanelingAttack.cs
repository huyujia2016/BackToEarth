using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenJetBanelingAttack : MonoBehaviour {
    public GameObject GreenJetBanelingShell;
    public Vector2 EndPos;

    public void Attack()
    {
        SoundManager._instance.Play("banelingAttack", this.transform.parent.GetComponent<AudioSource>());
        if (this.transform.parent.GetComponent<JetBaneling>().hp <= 0)
        {
            return;
        }
        GameObject go = Instantiate(GreenJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
        go.GetComponent<GreenJetBanelingShell>().Emit(EndPos);
        
    }
}
