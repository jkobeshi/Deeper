using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (!GetComponentInChildren<shock_anim>().frozen){
            gameObject.GetComponentInParent<Pathfinding.AIPath>().canMove = true;
        if(aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        }
        else{
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponentInParent<Pathfinding.AIPath>().canMove = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Light Beam" || collider.tag == "laser")
        {
            Debug.Log("hi");
            StartCoroutine(GetComponentInChildren<shock_anim>().shock_stop());
            Debug.Log("bee freezing");
        }
    }
}
