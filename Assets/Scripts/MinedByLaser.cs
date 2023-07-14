using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinedByLaser : MonoBehaviour
{
    bool breaking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "laser" && !breaking) {
            StartCoroutine(CrackBlock(gameObject));
        }
    }

    IEnumerator CrackBlock(GameObject playerObject) {
        breaking = true;
        float duration_sec = 0.5f;
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;

        SpriteRenderer rend = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            if (rend != null){
                rend.color = new Color(1f, 1f, 1f, progress);
            }
            yield return null;
        }
        breaking = false;
        gameObject.GetComponent<IsBreakable>().handleBreak(true);
    }
}
