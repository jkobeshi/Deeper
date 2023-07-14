using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip_with_player : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite left;
    public Sprite right;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x < gameObject.GetComponent<Transform>().position.x){
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
    }
}
