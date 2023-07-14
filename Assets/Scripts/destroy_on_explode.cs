using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_on_explode : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.tag == "Mine Radius" && coll.gameObject.GetComponent<mine_explod>().exploded){
            Destroy(gameObject);
        }
    }
}
