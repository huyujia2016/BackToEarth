using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBanelingNestShell : MonoBehaviour {

    private Vector2 startPos;
    private Vector2 targetPos;
    public int damage = 2;
    public float moveSpeed;
    private Animator anim;
    private float _moveTime;//移动到目标点需要的时间
    private float _timeCount = 0;//已经经过的时间
    private bool isCol = false;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        targetPos = GameManager._instance.Player.transform.position;
        float Xoffset = Random.Range(-2F, 2F);
        float Yoffset = Random.Range(-2F, 2F);
        targetPos.x += Xoffset;
        targetPos.y += Yoffset;
        _moveTime = Vector2.Distance(targetPos, startPos) / moveSpeed;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCol == false)
        {
            float distance = Vector2.Distance(targetPos, transform.position);
            if (distance < 0.35f)
            {
                Explode();
            }
            else
            {
                _timeCount += Time.deltaTime;
                transform.position = Vector2.Lerp(startPos, targetPos, _timeCount / _moveTime);
            }
        }
        
    }

    private void Explode()
    {
        SoundManager._instance.Play("banelingExplode", SoundManager._instance.GetComponent<AudioSource>());
        anim.SetTrigger("explodsion");
        float distance = Vector2.Distance(GameManager._instance.Player.transform.position, transform.position);
        if (distance < 1f)
        {
            isCol = true;
            GameManager._instance.Player.SendMessage("TakeDamageBeatBack", damage + ",CrystalBanelingNest,10,0.3", SendMessageOptions.DontRequireReceiver);
        }
        StartCoroutine(DestroyShell());
    }

    //动画播放结束后处理
    IEnumerator DestroyShell()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isCol==false)
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Obstacle" )
            {
                Explode();
                isCol = true;
            }
        }
        
    }
}
