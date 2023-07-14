using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeKill : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.tag == "Player" && ! GetComponentInChildren<shock_anim>().frozen && inventory.instance != null) {
            GameControl.instance.HandleDeathAndReset("Alien bee");
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.tag == "Mine Radius" && coll.gameObject.GetComponent<mine_explod>().exploded){
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
