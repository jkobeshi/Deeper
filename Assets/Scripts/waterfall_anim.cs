using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfall_anim : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer rende;
    void Start()
    {
        rende = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(animate_waterfall());
    }

    private IEnumerator animate_waterfall(){
        while (true){
            rende.sprite = sprites[0];
            yield return new WaitForSeconds(.3f);
            rende.sprite = sprites[1];
            yield return new WaitForSeconds(.3f);
            rende.sprite = sprites[2];
            yield return new WaitForSeconds(.3f);
        }
    }

    // Update is called once per frame
}
