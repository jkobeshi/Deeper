using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class random_sprite_non_flicker : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> sprites = new List<Sprite>();
    private
    // Update is called once per frame
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    public void randomize(){
        gameObject.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
