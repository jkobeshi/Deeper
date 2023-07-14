using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_not_destroy : MonoBehaviour
{
    public static do_not_destroy instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
}
