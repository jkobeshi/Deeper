using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class init_sfx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().value = GameControl.sound_effects_vol;
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(delegate {change_sfx ();});
    }

    public void change_sfx(){
        GameControl.sound_effects_vol = gameObject.GetComponent<Slider>().value;
    }
}
