using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_settings : MonoBehaviour
{
    public GameObject settings_menu;
    public bool activated = false;
    // Start is called before the first frame update

    // Update is called once per frame
    public void click(){
        if (!activated){
            settings_menu.SetActive(true);
            StartGame.disabled = true;
            activated = true;
            Debug.Log("activated");
        }
        else{
            settings_menu.SetActive(false);
            activated = false;
            StartGame.disabled = false;
            Debug.Log("deactivated");
        }
    }
}
