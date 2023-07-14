using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_moon : MonoBehaviour
{
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ending());
    }

    // Update is called once per frame
    private IEnumerator ending(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -.1f);
        yield return new WaitForSeconds(3f);
        StartCoroutine(credits.GetComponent<move_credits_up>().Start_Credits());
        yield return new WaitForSeconds(15f);
        Camera.main.gameObject.GetComponent<StartGame>().LoadGame();
    }
}
