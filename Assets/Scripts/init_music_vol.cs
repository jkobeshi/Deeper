using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class init_music_vol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().value = main_music.instance.GetComponent<AudioSource>().volume;
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(delegate {change_volume ();});
    }

    public void change_volume(){
        GameObject.Find("music_source").GetComponent<AudioSource>().volume = gameObject.GetComponent<Slider>().value;
    }
}
