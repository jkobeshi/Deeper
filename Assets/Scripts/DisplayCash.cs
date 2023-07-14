using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayCash : MonoBehaviour
{
    Text Cash;
    private float old_cash = 0f;
    void Start()
    {
        Cash = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(old_cash == inventory.instance.cash)
        {
            Cash.color = new Color(1, 1, 1, 1);
        }
        Cash.text = "$" + inventory.instance.cash.ToString(); 
        if (old_cash > inventory.instance.cash){
            Cash.color = new Color(1, 0, 0, 1);
            StartCoroutine(less_cash());
        }
        else if (old_cash < inventory.instance.cash){
            Cash.color = new Color(0, 1, 0, 1);
            StartCoroutine(more_cash());
        }
    }

    IEnumerator less_cash(){
        yield return new WaitForSeconds(.5f);
        old_cash = inventory.instance.cash;
    }

    IEnumerator more_cash(){
        yield return new WaitForSeconds(.5f);
        old_cash = inventory.instance.cash;
    }
}
