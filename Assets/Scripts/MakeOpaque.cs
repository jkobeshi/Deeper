using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeOpaque : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
    }
}