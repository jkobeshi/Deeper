using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBreakable : MonoBehaviour
{
    public float hardness = 1;
    public float miningTime = 0;
    SpriteRenderer Sprite;
    public List<Sprite> sprite_pool = new List<Sprite>();
    public int blockLayer;
    private void Start()
    {
        miningTime = hardness * 0.45f;
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.sprite = sprite_pool[Random.Range(0, sprite_pool.Count)];
        if(gameObject.transform.position.y % 2 == 0)
            Sprite.sortingOrder = ((((((int)gameObject.transform.position.x % 4) - 2) % 4) + 4) % 4) + (blockLayer * 4);
        else
            Sprite.sortingOrder = ((((int)gameObject.transform.position.x % 4) + 4) % 4) + (blockLayer * 4);
    }

    public void handleBreak(bool is_player) {
        if (is_player){
            if (inventory.instance != null){
            inventory.instance.Collect(this.gameObject);
            }
        }
        UpdateGraph.instance.updtGraph();
        Destroy(this.gameObject);
    }
}
