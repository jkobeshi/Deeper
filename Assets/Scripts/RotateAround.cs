using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public SpriteRenderer curSprite;
    [SerializeField] Sprite Drill1;
    [SerializeField] Sprite Drill2;
    [SerializeField] Sprite Drill3;
    private void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
        curSprite.sprite = null;
    }

    public void Update()
    {
        if (inventory.instance != null){
        if (inventory.instance.hardness == 0)
            curSprite.sprite = null;
        else if (inventory.instance.hardness == 1)
            curSprite.sprite = Drill1;
        else if (inventory.instance.hardness == 2)
            curSprite.sprite = Drill2;
        else if (inventory.instance.hardness == 3)
            curSprite.sprite = Drill3;
        }
        else{
            curSprite.sprite = Drill1;
        }
    }
}
