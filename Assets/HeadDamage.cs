using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDamage : MonoBehaviour
{
    public int health = 3;
    public AudioClip worm_dmg;
    public GameObject light_flash;
    private Color orig_light_flash;
    // Start is called before the first frame update

    void Start (){
        orig_light_flash = light_flash.GetComponent<Light>().color;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Mine Radi"){
            if(!other.gameObject.GetComponent<mine_explod>().exploded) {
                Debug.Log("WHAT IS THIS");
                StartCoroutine(other.gameObject.GetComponent<mine_explod>().explode());
                if(gameObject.tag == "WormHead") {
                    health -= 1;
                    StartCoroutine(flash_dmg());
                    AudioSource.PlayClipAtPoint(worm_dmg, Camera.main.transform.position, GameControl.sound_effects_vol * .5f);
                    if (health == 0){
                    gameObject.transform.parent.gameObject.GetComponent<RandomMove>().enabled = false;
                    gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    foreach (GameObject temp in GameObject.FindGameObjectsWithTag("onWormKill")){
                        Destroy(temp);
                    }
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Player"){
            GameControl.instance.HandleDeathAndReset("Giant Worm");
        }
    }
    private IEnumerator flash_dmg(){
        light_flash.GetComponent<Light>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds (.3f);
        light_flash.GetComponent<Light>().color = orig_light_flash;
        yield return new WaitForSeconds(.2f);
        light_flash.GetComponent<Light>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(.3f);
        light_flash.GetComponent<Light>().color = orig_light_flash;
    }
}
