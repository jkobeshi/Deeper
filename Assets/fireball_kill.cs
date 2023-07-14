using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_kill : MonoBehaviour
{
    private void Update()
    {
        if(gameObject.transform.position.y >= -159)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Player"){
            GameControl.instance.HandleDeathAndReset("Giant Worm");
        }
    }

    // Update is called once per frame
}
