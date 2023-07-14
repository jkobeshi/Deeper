using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            inventory.instance.NumBoughtForUpgrades[6]++;
            GameControl.instance.shield_max += 1;
            GameControl.instance.shield_health = GameControl.instance.shield_max;
            coll.GetComponent<inventory>().shieldBought = true;
            coll.gameObject.GetComponent<MessageController>().DisplayMessage("Shield Upgraded", Color.yellow);
        }
    }
}
