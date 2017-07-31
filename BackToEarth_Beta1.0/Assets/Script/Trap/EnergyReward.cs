using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyReward : MonoBehaviour {

    public int AddValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            GameManager._instance.RewardEnergy += AddValue;
            MessageManager._instance.ShowMessage("得到额外能量奖励：+"+ AddValue.ToString(),3);
            SoundManager._instance.Play("getProp", SoundManager._instance.audioSource);
            Destroy(this.gameObject);
        }
    }
}
