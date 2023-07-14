using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenIndex : MonoBehaviour
{
    public int index = 0;
    Text nameOfIndex;
    Text cost;
    void Start()
    {
        cost = gameObject.transform.GetChild(0).GetComponent<Text>();
        cost.text = "$" + inventory.instance.utilities[index].cost.ToString();
        nameOfIndex = GetComponent<Text>();
        nameOfIndex.text = inventory.instance.utilities[index].name;
        if (inventory.instance.utilities[index].type == 'q')
        {
            nameOfIndex.text = "x1 " + inventory.instance.utilities[index].name;
        }
        else if (inventory.instance.utilities[index].type == 'u')
        {
            nameOfIndex.text = "++ " + inventory.instance.utilities[index].name;
        }
    }
}
