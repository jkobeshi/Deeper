using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class init_screen_shake : MonoBehaviour
{
    // Start is called before the first frame update
        void Start()
    {
        gameObject.GetComponent<Slider>().value = GameControl.screen_shake_mult;
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(delegate {change_shake ();});
    }

    public void change_shake(){
        GameControl.screen_shake_mult = gameObject.GetComponent<Slider>().value;
    }
}
