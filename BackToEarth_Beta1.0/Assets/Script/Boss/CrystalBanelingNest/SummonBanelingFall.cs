using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBanelingFall : MonoBehaviour {

    private CapsuleCollider2D col;
    public float WaitTime;
    private float waitTimer = 0;
    private Rigidbody2D rb;
    public float LandingSpeed;
    private PolyNavAgent agent;
    private Animator anim;
    [HideInInspector]
    public float x;

    private bool isLanding;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        col.isTrigger = true;
        rb = this.GetComponent<Rigidbody2D>();
        isLanding = true;
        agent = this.GetComponent<PolyNavAgent>();
        agent.enabled = false;
        anim = transform.Find("Anim").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (isLanding)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= WaitTime)
            {
                rb.velocity = new Vector2(0, -1* LandingSpeed);
            }
        }
        else
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= WaitTime)
            {
                this.gameObject.GetComponent<EnemyBase>().isActivated = true;
            }
        }
    }

    public void Arrived()
    {
        waitTimer = 0;
        isLanding = false;
        rb.velocity = Vector2.zero;
        this.GetComponent<CapsuleCollider2D>().isTrigger = false;
        agent.enabled = true;
        if (Random.Range(-1F, 1F) > 0)
        {
            anim.SetTrigger("idle_left");
            anim.SetTrigger("left_idle");
        }
        else
        {
            anim.SetTrigger("idle_right");
            anim.SetTrigger("right_idle");
        }
        GameManager._instance.AddEnemy(this.gameObject);
    }
}
