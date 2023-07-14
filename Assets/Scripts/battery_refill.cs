using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battery_refill : MonoBehaviour
{
    public float percentRefill = 100f;
    float refill_amount;
    Vector3 respawnPoint;
    public AudioClip clip;

    private void Start()
    {
        respawnPoint = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.name == "Player" && inventory.instance != null) {
            refill_amount = coll.gameObject.GetComponentInChildren<light_dimmer>().max_light_health * (percentRefill / 100);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, GameControl.sound_effects_vol * .5f);
            if ((coll.gameObject.GetComponentInChildren<light_dimmer>().light_health + refill_amount) >
                coll.gameObject.GetComponentInChildren<light_dimmer>().max_light_health)
            {
                coll.gameObject.GetComponentInChildren<light_dimmer>().light_health = coll.gameObject.GetComponentInChildren<light_dimmer>().max_light_health;
            }
            else
            {
                coll.gameObject.GetComponentInChildren<light_dimmer>().light_health += refill_amount;
            }
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if (!GameControl.instance.Reloading)
        {
            if (SceneManager.GetActiveScene().name == "main")
            {
                GameControl.batteries_to_respawn.Add(respawnPoint);
            }
        }
    }
}
