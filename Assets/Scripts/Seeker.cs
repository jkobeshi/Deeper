using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    
    public bool moveSeeker;
    public bool aggroSeeker;
    public float speedCrawl;
    public GameObject lightning;

    Vector3 startpos;
    Vector3 endpos;

    private float timeScale = 0;
    private GameObject playerMain;
    private float phase = 0;
    private float journeyLength;
    private float phaseDirection = 1;
    private Rigidbody2D rb;
    private Animator ani;
    private bool path = false;
    private bool flyRight = true;

    // Start is called before the first frame update
    void Start()
    {
        moveSeeker = true;
        aggroSeeker = false;
        playerMain = GameObject.Find("Player");
        startpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endpos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        rb.gravityScale = 1;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(moveSeeker);
        if (moveSeeker && !aggroSeeker)
        {

            Walk();
        }
        else if (aggroSeeker)
        {
            
            ani.SetTrigger("Fly");
            ani.ResetTrigger("Walk");
            moveSeeker = false;
            rb.gravityScale = 0;
            Chase();
           
        }
        if (Vector3.Distance(transform.position, playerMain.transform.position) < 3f)
        {
            aggroSeeker = true;
            if (Vector3.Distance(transform.position, playerMain.transform.position) < 1.75f)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            Debug.Log(flyRight);
            // Debug.Log(playerMain.transform.position.x + " " + transform.position.x);
            if (playerMain.transform.position.x - transform.position.x > 2.0f &&
                playerMain.transform.position.x - transform.position.x < 2.5f)
            {
                flyRight = true;
            }
            else if (transform.position.x - playerMain.transform.position.x > 2.0f &&
              transform.position.x - playerMain.transform.position.x < 2.5f)
            {
                flyRight = false;
            }

        }
        if (Vector3.Distance(transform.position, playerMain.transform.position) > 4f)
        {
            aggroSeeker = false;
            moveSeeker = true;
            rb.gravityScale = 1;
            ani.SetTrigger("Walk");
            ani.ResetTrigger("Fly");

        }
        if (GameObject.Find("Player").GetComponent<Transform>().position.x < transform.position.x){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            lightning.GetComponent<SpriteRenderer>().flipX = true;
            lightning.GetComponent<Transform>().localPosition = new Vector3(-1 * Mathf.Abs(lightning.GetComponent<Transform>().localPosition.x), lightning.GetComponent<Transform>().localPosition.y, 0);
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            lightning.GetComponent<SpriteRenderer>().flipX = false;
            lightning.GetComponent<Transform>().localPosition = new Vector3(Mathf.Abs(lightning.GetComponent<Transform>().localPosition.x), lightning.GetComponent<Transform>().localPosition.y, 0);
        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Block")
        {
            moveSeeker = true;
            startpos = transform.position;
        }
    }

    void OnCollisionExit(Collision other)
    {
        moveSeeker = false;
    }
    void Chase()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 flyPt = new Vector3(playerPos.x - 1.25f, playerPos.y + 0.5f, 0);
        Vector3 flyPt2 = new Vector3(playerPos.x + 1.25f, playerPos.y + 0.5f, 0);
        if (flyRight)
        {
            transform.position = Vector3.Lerp(transform.position, flyPt2, phase);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, flyPt, phase);
        }
        phase += (Time.deltaTime * 0.0025f);
    }
    void Walk()
    {
        Vector3 move = new Vector3(0.1f, 0, 0);
        transform.position += (move * phaseDirection * speedCrawl);
        timeScale += Time.deltaTime;
        if (timeScale >= 3)
        {
            phaseDirection *= -1;
            timeScale = 0;
        }
    }

    public IEnumerator Pathing()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 flyPt = new Vector3(playerPos.x - 1, playerPos.y + 0.5f, 0);
        Vector3 flyPt2 = new Vector3(playerPos.x + 1, playerPos.y + 0.5f, 0);
        transform.position = Vector3.Lerp(transform.position, flyPt, phase);
        phase += (Time.deltaTime * 0.0025f);

        yield return new WaitForSeconds(2.0f);

        transform.position = Vector3.Lerp(transform.position, flyPt2, phase);
        phase += (Time.deltaTime * 0.0025f);

        yield return new WaitForSeconds(2.0f);
        path = false;
    }
}
