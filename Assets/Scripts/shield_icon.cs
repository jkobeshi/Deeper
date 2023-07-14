using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_icon : MonoBehaviour
{
    public Sprite Torn;
    public Sprite Shield;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        rend = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (inventory.instance.shieldBought){
            if (GameControl.instance.shield_health == GameControl.instance.shield_max){
                rend.sprite = Shield;
            }
            else if (GameControl.instance.shield_health == 0){
                rend.sprite = Torn;
            }
        }
        else{
            rend.sprite = null;
        }
    }
}
