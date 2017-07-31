using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanelingTrap : MonoBehaviour {

    public int damage;
    private bool isCollider = false;
    public Vector2 PosA;
    public Vector2 PosB;
    //private Vector2 StartPos;

    public float _moveTime;//从A点到B点的时间
    private float _timeCount = 0;//已经经过的时间

    void Update () {
        if (isCollider==false)
        {
            if (_timeCount>= _moveTime)
            {
                Vector2 pos = PosA;
                PosA = PosB;
                PosB = pos;
                _timeCount = 0;
            }
            _timeCount += Time.deltaTime;
            transform.position = Vector2.Lerp(PosA, PosB, _timeCount / _moveTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isCollider==false)
        {
            isCollider = true;
            SoundManager._instance.Play("banelingExplode", this.GetComponent<AudioSource>());
            this.GetComponent<Animator>().SetTrigger("explodsion");
            Tina._instance.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            StartCoroutine(DestoryTrap());
        }
    }

    IEnumerator DestoryTrap()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }



}
