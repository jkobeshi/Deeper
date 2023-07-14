using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayUtility : MonoBehaviour
{
    Text Util;
    void Start()
    {
        Util = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.instance.firstItemAcquired) {
            if (inventory.instance.utilities[inventory.instance.current_index].type == 'q')
            {
                Util.text = "Utility: " + inventory.instance.utilities[inventory.instance.current_index].name +
                    ", Qty: " + inventory.instance.Quant[inventory.instance.current_index];
            }
            else
            {
                Util.text = "Utility: " + inventory.instance.utilities[inventory.instance.current_index].name;
            }
        }

    }
}
