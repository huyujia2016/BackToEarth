using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingDamageTrigger : MonoBehaviour {

    private bool isCollided = false;
    public string LandingAttackAudio;
    public string AcceptObjectName;
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == AcceptObjectName && isCollided == false)
        {
            if (other.gameObject.GetComponent<EnemyBase>().isActivated == false)
            {
                isCollided = true;
                LandingAttack();
                other.gameObject.GetComponent<SummonBanelingFall>().Arrived();
                Destroy(this.gameObject);
            }
        }
    }

    public void LandingAttack()
    {
        SoundManager._instance.Play(LandingAttackAudio, SoundManager._instance.GetComponent<AudioSource>());
        Vector2 pos = this.transform.InverseTransformPoint(GameManager._instance.Player.transform.position);
        
        if (pos.x > -0.36f && pos.x < 0.396f && pos.y > -0.25f && pos.y < 0.4f)
        {
            Tina._instance.SendMessage("TakeDamage", CrystalBanelingNest._instance.LandingDamage, SendMessageOptions.DontRequireReceiver);
        }
        
    }
}
