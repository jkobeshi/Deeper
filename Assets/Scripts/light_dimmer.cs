using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class light_dimmer : MonoBehaviour
{
    public float light_health = 35f;
    public float time_when_dims = 20f;
    public float light_spot_ang = 40f;
    public bool in_light = true;
    public bool no_light = false;
    public bool refill = false;
    public float max_light_health;
    public AudioClip refill_sound;

    // Start is called before the first frame update
    void Start()
    {
        max_light_health = light_health;
        gameObject.GetComponent<Light>().spotAngle = light_spot_ang;
        StartCoroutine(dimmer());
    }

    void Update(){
        if (in_light)
        {
            no_light = false;
            if (refill)
            {
                light_health = max_light_health;
            }
        }
        if (!no_light){
            if ((light_health < 0)){
                GameControl.instance.HandleDeathAndReset("Running out of light");
                no_light = true;
                light_health = max_light_health;
            }
            else if (light_health > time_when_dims){
                gameObject.GetComponent<Light>().spotAngle = light_spot_ang;
            }
            else {
                gameObject.GetComponent<Light>().spotAngle = light_health/max_light_health * light_spot_ang;
            }
        }
    }

    private IEnumerator dimmer(){
        while (true){
            if (!in_light){
                light_health -= 1f;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
}
