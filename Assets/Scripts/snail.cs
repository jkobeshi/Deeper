using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snail : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve xCurve;
    public float speedCrawl;
    public bool tamed;

    private Rigidbody2D rb;
    private bool started;
    private float timeElapsed = 0;

    private bool startled = false;
    private float timeScale = 0;
    private Vector3 startPosition;
    private float phaseDirection = 1;
    private GameObject playerMain;
    private Animator ani;
    void Start()
    {
        playerMain = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        if ((Vector3.Distance(transform.position, playerMain.transform.position) > 3f && !startled) || tamed)
        {
            Walk();
        }
        else
        {
            startled = true;
            ani.SetTrigger("Hide");
            ani.ResetTrigger("Walk");
        }
    }
    void Walk()
    {
        timeElapsed += Time.deltaTime;

       if (timeElapsed > 2) timeElapsed = 0;
        float xflo = xCurve.Evaluate(timeElapsed);

        Vector3 move = new Vector3(xflo, 0, 0);
        transform.position += (move * phaseDirection * speedCrawl);
        timeScale += Time.deltaTime;
        if (timeScale >= 4)
        {
            phaseDirection *= -1;
            if (gameObject.GetComponent<SpriteRenderer>().flipX == true){
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else{
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            timeScale = 0;
        }
    }
}
