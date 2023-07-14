using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_filler : MonoBehaviour
{
    public List<Sprite> light_sprites = new List<Sprite>();
    private SpriteRenderer rende;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        rende = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<light_dimmer>() != null)
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<light_dimmer>().in_light)
            {
                rende.enabled = true;
                float max_light_hp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<light_dimmer>().max_light_health;
                float current_light_hp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<light_dimmer>().light_health;
                float ratio = current_light_hp / max_light_hp;
                int ind_to_access = (int)(ratio * light_sprites.Count);
                if (ind_to_access >= light_sprites.Count)
                {
                    ind_to_access = light_sprites.Count - 1;
                }
                if (!GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<light_dimmer>().no_light){
                rende.sprite = light_sprites[ind_to_access];
                }
            }
            else
            {
                rende.enabled = false;
            }
        }
    }
}
