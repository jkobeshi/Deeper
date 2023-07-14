using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shock_anim : MonoBehaviour
{
    public Sprite first_sprite;
    public Sprite second_sprite;
    public Sprite third_sprite;
    public float shock_time = 5f;
    public bool frozen;

    private SpriteRenderer rende;
    // Start is called before the first frame update
    void Start()
    {
        rende = gameObject.GetComponent<SpriteRenderer>();
        frozen = false;
    }

    // Update is called once per frame
    private IEnumerator shocked(){
        while (true){
            rende.sprite = first_sprite;
            rende.color = new Color (1, 1, 1, Random.Range(30, 75) * .01f);
            yield return new WaitForSeconds(.03f);
            if (frozen == false){
                break;
            }
            rende.sprite = second_sprite;
            yield return new WaitForSeconds(.03f);
            if (frozen == false){
                break;
            }
            rende.sprite = third_sprite;
            yield return new WaitForSeconds(.03f);
            if (frozen == false){
                break;
            }
        }
    }

    public IEnumerator shock_stop(){
        Debug.Log("in the shock");
        frozen = true;
        StartCoroutine(shocked());
        yield return new WaitForSeconds(shock_time);
        frozen = false;
        rende.color = new Color (1, 1, 1, 0);
    }
}
