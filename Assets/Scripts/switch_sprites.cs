using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_sprites : MonoBehaviour
{
    public Sprite sprite_one;
    public Sprite sprite_two;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(swapper());
    }

    // Update is called once per frame
    IEnumerator swapper(){
        while(true){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_one;
            yield return new WaitForSeconds(.75f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_two;
            yield return new WaitForSeconds(.75f);
        }
    }
}
