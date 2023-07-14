using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_sprite : MonoBehaviour
{
    public List<Sprite> sprites;
    public float flicker_rate = .3f;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(flicker());
    }

    // Update is called once per frame
    private IEnumerator flicker(){
        while (true){
            rend.sprite = sprites[Random.Range(0, sprites.Count)];
            yield return new WaitForSeconds(flicker_rate);
        }
    }
}
