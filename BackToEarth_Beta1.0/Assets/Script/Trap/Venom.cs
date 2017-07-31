using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour {

    public int Damage;
    public float DamageRate;
    private float damageRateTimer = 0;

    private bool isActivated = false;

	
	// Update is called once per frame
	void Update () {
        if (isActivated)
        {
            if (damageRateTimer>=DamageRate)
            {
                GameManager._instance.Player.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
                damageRateTimer -= DamageRate;
            }
            else
            {
                damageRateTimer += Time.deltaTime;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            isActivated = true;
            if ((Tina._instance.MoveSpeed+ DataSet.Instance().MoveSpeedAdditional) >= 3f)
            {
                DataSet.Instance().MoveSpeedAdditional -= 2.5F;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = false;
            damageRateTimer = 0;
            if ((Tina._instance.MoveSpeed + DataSet.Instance().MoveSpeedAdditional) <= 2.5f)
            {
                DataSet.Instance().MoveSpeedAdditional += 2.5F;
            }
        }
    }

}
