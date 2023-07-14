using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_reset : MonoBehaviour
{
    public AudioClip refill_sound;
    private bool in_safezone = false;
    private bool in_torch = false;
    public bool light_upgrade = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<light_dimmer>().in_light = true;
        gameObject.GetComponentInChildren<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (in_safezone){
            if (gameObject.GetComponentInChildren<light_dimmer>() != null)
            {
                gameObject.GetComponentInChildren<light_dimmer>().in_light = true;
                gameObject.GetComponentInChildren<light_dimmer>().refill = true;
                GameObject.FindGameObjectWithTag("light_upgrade").GetComponent<SpriteRenderer>().enabled =false;
            }
            if (gameObject.GetComponentInChildren<Light>() != null)
            {
                gameObject.GetComponentInChildren<Light>().enabled = false;
            }
        }
        else if (in_torch)
        {
            if (gameObject.GetComponentInChildren<light_dimmer>() != null)
            {
                gameObject.GetComponentInChildren<light_dimmer>().in_light = true;
                GameObject.FindGameObjectWithTag("light_upgrade").GetComponent<SpriteRenderer>().enabled = false;
            }
            if (gameObject.GetComponentInChildren<Light>() != null)
            {
                gameObject.GetComponentInChildren<Light>().enabled = false;
            }
        }
        else {
            if (gameObject.GetComponentInChildren<Light>() != null)
            {
                gameObject.GetComponentInChildren<Light>().enabled = true;
            }
            if (gameObject.GetComponentInChildren<light_dimmer>() != null)
            {
                gameObject.GetComponentInChildren<light_dimmer>().in_light = false;
                gameObject.GetComponentInChildren<light_dimmer>().refill = false;
                if (light_upgrade){
                    GameObject.FindGameObjectWithTag("light_upgrade").GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "SafeZone") {
            Debug.Log("In zone");
            // The message is kinda buggy for the first safe zone idk why
            if (coll.gameObject.name != "Above Ground Safezone") {
                GameObject.Find("Player").GetComponent<MessageController>().DisplayMessage("Safe zone entered: Light refilled", Color.yellow);
            }
            //AudioSource.PlayClipAtPoint(refill_sound, Camera.main.transform.position, .5f);
            in_safezone = true;
        }
        if(coll.gameObject.tag == "Torch")
        {
            Debug.Log("in_torch true");
            in_torch = true;
            GameObject.Find("Player").GetComponent<MessageController>().DisplayMessage("Torch zone Entered", Color.yellow);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if (coll.gameObject.tag == "SafeZone"){
            Debug.Log("out of zone");
            in_safezone = false;
        }
        if(coll.gameObject.tag == "Torch")
        {
            Debug.Log("in_torch false");
            in_torch = false;
        }
    }
}
