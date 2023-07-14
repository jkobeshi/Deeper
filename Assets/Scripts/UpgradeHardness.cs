using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHardness : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inventory.instance.NumBoughtForUpgrades[7]++;
            inventory.instance.hardness++;
            collision.gameObject.GetComponent<MessageController>().DisplayMessage("Drill Hardness +", new Color(0, 1, 0, 1));
            Destroy(gameObject);
        }
    }
}
