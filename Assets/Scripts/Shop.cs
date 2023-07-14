using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Animator Anim;
    public List<int> ItemsInShop;
    public List<int> MaxQuant; //MaxQuantity at index I a person can buy
    public List<int> QuantBought;
    private void Start()
    {
        while (MaxQuant.Count != ItemsInShop.Count)
        {
            MaxQuant.Add(1);
        }
        while (QuantBought.Count != MaxQuant.Count)
        {
            QuantBought.Add(0);
        }
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector2.Distance(PlayerMovement.instance.gameObject.transform.position, gameObject.transform.position) > 5)
        {
            Anim.SetBool("Off", true);
        }
        else
        {
            Anim.SetBool("Off", false);
        }
    }
}
