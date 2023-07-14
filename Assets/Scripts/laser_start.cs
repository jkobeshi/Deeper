using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_start : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip laser_audio;
    private SpriteRenderer rend;
    private SpriteRenderer player_rend;
    void Start()
    {
        player_rend = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        gameObject.transform.position = GameObject.FindGameObjectWithTag("PlayerDrill").transform.position;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GameObject.FindGameObjectWithTag("PlayerDrill").transform.rotation.eulerAngles.z));
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z) * 4f, gameObject.transform.position.y + Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.eulerAngles.z) * 4f);
        StartCoroutine(laser_anim());
        StartCoroutine(start_laser());
    }

    private IEnumerator laser_anim(){
        while(true){
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            yield return new WaitForSeconds(.03f);
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(.03f);
            gameObject.GetComponentInChildren<SpriteRenderer>().flipY = true;
            yield return new WaitForSeconds(.03f);
            gameObject.GetComponentInChildren<SpriteRenderer>().flipY = false;
            yield return new WaitForSeconds(.03f);
        }
    }

    // Update is called once per frame
    private IEnumerator start_laser(){
        GameObject.Find("Player").GetComponent<PlayerMovement>().resMov = true;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player").GetComponent<RotateDrill>().locked = true;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0;
        float duration_sec = 0.5f;
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            player_rend.color = new Color(Mathf.Lerp(1, .8f, progress), Mathf.Lerp(1, .2f, progress), Mathf.Lerp(1, .6f, progress), 1);
            rend.color = new Color(1, 1, 1, Mathf.Lerp(0, .7f, progress));
            yield return null;
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        player_rend.color = new Color(1, 1, 1, 1);
        Camera.main.gameObject.GetComponent<ScreenShakeController>().StartShake(1f, .1f);
        AudioSource.PlayClipAtPoint(laser_audio, Camera.main.transform.position, GameControl.sound_effects_vol * .5f);
        yield return new WaitForSeconds(1f);
        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 1;
        GameObject.Find("Player").GetComponent<PlayerMovement>().resMov = false;
        GameObject.Find("Player").GetComponent<RotateDrill>().locked = false;
        Destroy(gameObject);
    }
}
