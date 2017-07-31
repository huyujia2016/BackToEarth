using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenJetBanelingShell : MonoBehaviour {
    private bool isCollided = false;
    private bool isEmit = false;
    public int damage;
    public Vector2 PosStart;
    public Vector2 PosEnd;
    //private Vector2 StartPos;
    public GameObject Venom;
    private bool createVenom = false;

    public float _moveTime;//从A点到B点的时间
    private float _timeCount = 0;//已经经过的时间

    public void Emit(Vector2 endPos)
    {
        PosStart = transform.position;
        PosEnd = endPos;
        isEmit = true;
    }

    void Update()
    {
        if (isEmit)
        {
            if (isCollided == false)
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= _moveTime)
                {
                    Explode();
                }
                transform.position = Vector2.Lerp(PosStart, PosEnd, _timeCount / _moveTime);
            }
        }
    }

    private void Explode()
    {
        isEmit = false;
        isCollided = true;
        SoundManager._instance.Play("banelingExplode", this.GetComponent<AudioSource>());
        this.GetComponent<Animator>().SetTrigger("explodsion");
        float distance = Vector2.Distance(GameManager._instance.Player.transform.position, transform.position);
        if (distance < 1.3f)
        {
            GameManager._instance.Player.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        StartCoroutine(DestoryShell());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isCollided == false)
        {
            createVenom = true;
            Explode();
        }
    }

    IEnumerator DestoryShell()
    {
        yield return new WaitForSeconds(0.5f);
        if (createVenom)
        {
            Instantiate(Venom, this.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
