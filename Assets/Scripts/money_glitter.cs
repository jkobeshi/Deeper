using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money_glitter : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(glitter());
    }

    // Update is called once per frame
    private IEnumerator glitter(){
        while (true){
            index++;
            index = index % sprites.Count;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
            if (index == 0){
                yield return new WaitForSeconds(2f);
            }
            else{
            yield return new WaitForSeconds(.03f);
            }
        }
    }
}
