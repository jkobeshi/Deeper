using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine_shock : MonoBehaviour
{
    public GameObject anchor_block;
    public bool boss_mine = false;
    private Vector3 respawn_pont;

    void Start(){
        respawn_pont = gameObject.transform.position;
    }
    void Update(){
        if (anchor_block == null){
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Light Beam" || coll.tag == "laser")
        {
            StartCoroutine(gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<shock_anim>().shock_stop());
            Debug.Log("freezing");
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        Debug.Log("exploding");
        StartCoroutine(GetComponentInChildren<mine_explod>().explode());
    }

    void OnDestroy(){
        if (!GameControl.instance.Reloading && !boss_mine)
        {
            GameControl.mines_to_respawn.Add(respawn_pont);
        }
        else if (boss_mine && !GameControl.instance.Reloading){
            GameControl.boss_mines_to_respawn.Add(respawn_pont);
        }
    }
}
