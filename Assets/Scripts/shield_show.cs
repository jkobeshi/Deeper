using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class shield_show : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite broken;
    public Sprite full;

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.shield_max == 3){
            gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = broken;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = broken;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = broken;
        }
        else if (GameControl.instance.shield_max == 2){
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = broken;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = broken;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        }
        else if (GameControl.instance.shield_max == 1){
            gameObject.GetComponentInChildren<Text>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = broken;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        }
        else{
           gameObject.GetComponentInChildren<Text>().enabled = false;
           gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
           gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
           gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
        }
        if (GameControl.instance.shield_health == 3){
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = full;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = full;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = full;
        }
        else if (GameControl.instance.shield_health == 2){
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = full;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = full;
        }
        else if (GameControl.instance.shield_health == 1){
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = full;
        }
        
    }
}
