using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jolt : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite first_sprite;
    public Sprite second_sprite;
    public Sprite third_sprite;
    private SpriteRenderer rend;
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    public void call_jolter(){
        StartCoroutine(jolter());
    }
    // Update is called once per frame
    private IEnumerator jolter(){
        while(true){
            rend.sprite = first_sprite;
            rend.color = new Color (1, 1, 1, Random.Range(30, 75) * .01f);
            yield return new WaitForSeconds(.03f);
            rend.sprite = second_sprite;
            yield return new WaitForSeconds(.03f);
            rend.sprite = third_sprite;
            yield return new WaitForSeconds(.03f);
        }
    }
}
