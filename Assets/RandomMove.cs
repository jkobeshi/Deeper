using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    bool dashing = false, Moving = false, Return = false;
    public GameObject fireball;
    public GameObject head;

    public GameObject leftbound, rightbound, low, mid, high;
    void FixedUpdate()
    {
        if (gameObject.transform.position.x <= leftbound.transform.position.x || gameObject.transform.position.x >= rightbound.transform.position.x)
        {
            Return = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            StartCoroutine(ReturnFalse());
        }
        else
        {
            if (!Moving && !dashing && !Return)
            {
                if (PlayerMovement.instance.gameObject.transform.position.y <= (high.transform.position.y + 1))
                {

                    if (Mathf.Abs(gameObject.transform.position.x - PlayerMovement.instance.gameObject.transform.position.x) >= 3.2f)
                    {
                        if (gameObject.transform.position.x < PlayerMovement.instance.gameObject.transform.position.x)
                        {
                            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(3f, 5f), 0f);
                        }
                        else
                        {
                            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5f, -3f), 0f);
                        }
                    }
                    else
                    {
                        StartCoroutine(PullBackThenDash());
                    }
                }
                else
                {
                    StartCoroutine(MoveLeftRight());
                }
            }
        }
    }
    IEnumerator ReturnFalse()
    {
        yield return new WaitForSeconds(0.1f);
        Return = false;
    }
    IEnumerator MoveLeftRight()
    {
        Moving = true;
        float rando3 = Random.Range(-1f, 1f);
        if (rando3 > 0f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(3f, 5f), 0f);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5f, -3f), 0f);
        }
        yield return new WaitForSeconds(1.5f);

        int rando2 = Random.Range(-1, 3);
        if (rando2 == 1f)
        {
            StartCoroutine(ShootFireballs());
        }
        yield return new WaitForSeconds(0.5f);
        Moving = false;
    }


    IEnumerator PullBackThenDash() {
        dashing = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);

        while (gameObject.transform.position.y > low.transform.position.y) {
            yield return null;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6f);

        while (gameObject.transform.position.y < high.transform.position.y) {
            yield return null;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6f);

        while (gameObject.transform.position.y > mid.transform.position.y) {
            yield return null;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        dashing = false;
    }

    IEnumerator ShootFireballs() {
        dashing = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);

        while (gameObject.transform.position.y > low.transform.position.y) {
            yield return null;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 5f);    
        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 5f);
        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);      

        yield return new WaitForSeconds(0.5f);

        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 5f);    
        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 5f);
        Instantiate(fireball, head.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);

        while (gameObject.transform.position.y < mid.transform.position.y) {
            yield return null;
        }
        dashing = false;
    }
}
