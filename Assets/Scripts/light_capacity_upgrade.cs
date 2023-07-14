using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_capacity_upgrade : MonoBehaviour
{
    public GameObject light_upgrade_sprite;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Player"){
            inventory.instance.NumBoughtForUpgrades[5]++;
            coll.GetComponentInChildren<light_dimmer>().max_light_health *=2;
            coll.GetComponentInChildren<light_dimmer>().time_when_dims *= 2;
            coll.GetComponentInChildren<light_dimmer>().light_spot_ang *= 2;
            coll.GetComponent<light_reset>().light_upgrade = true;
            coll.gameObject.GetComponent<MessageController>().DisplayMessage("Light Capacity+", Color.yellow);
        }
    }
    // Update is called once per frame
}
