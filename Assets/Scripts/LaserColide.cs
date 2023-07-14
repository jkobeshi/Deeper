using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserColide : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Light Beam" || coll.tag == "laser")
        {
            StartCoroutine(GetComponent<shock_anim>().shock_stop());
            Debug.Log("freezing");
        }
    }
}
