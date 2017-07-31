using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedJetBanelingAttack : MonoBehaviour {

    int JetCount=0;
    public GameObject RedJetBanelingShell;

    public void Attack()
    {
        SoundManager._instance.Play("banelingAttack", this.transform.parent.GetComponent<AudioSource>());
        if (this.transform.parent.GetComponent<JetBaneling>().hp <= 0)
        {
            return;
        }
        if (JetCount==3)
        {
            JetCount = 1;
        }
        else
        {
            JetCount++;
        }
        if (JetCount == 1)
        {
            GameObject go = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go.transform.position.x - 5, go.transform.position.y));
        }
        else if(JetCount == 2)
        {
            GameObject go1 = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go1.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go1.transform.position.x - 5, go1.transform.position.y+0.5f));

            GameObject go2 = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go2.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go2.transform.position.x - 5, go2.transform.position.y- 0.5f));
        }
        else
        {
            GameObject go = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go.transform.position.x - 5, go.transform.position.y));

            GameObject go1 = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go1.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go1.transform.position.x - 5, go1.transform.position.y + 1));

            GameObject go2 = Instantiate(RedJetBanelingShell, transform.Find("EmissionPoint").position, Quaternion.identity);
            go2.GetComponent<RedJetBanelingShell>().Emit(new Vector2(go2.transform.position.x - 5, go2.transform.position.y - 1));
        }
    }
}
