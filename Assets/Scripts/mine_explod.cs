using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine_explod : MonoBehaviour
{
    public AudioClip beeping;
    public AudioClip explosion;
    public bool exploded = false;
    public float duration_sec = .5f;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(beep());   
    }

    public IEnumerator explode(){
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;
        while (progress < 1.0f)
        {
             progress = (Time.time - initial_time) / duration_sec;
             gameObject.GetComponentInChildren<Light>().intensity = Mathf.Lerp(0, 30, progress);
             if (gameObject.GetComponentInChildren<shock_anim>().frozen)
             {
             gameObject.GetComponentInChildren<Light>().intensity = 0;
             yield break;
             }
             yield return null;
         }
        if (gameObject.GetComponentInChildren<shock_anim>().frozen){
            gameObject.GetComponentInChildren<Light>().intensity = 0;
            yield break;
        }
        else if (exploded == false)
        {
            exploded = true;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            gameObject.GetComponentInChildren<Light>().color = new Color (.8f, .8f, .4f, 1);
            AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position, GameControl.sound_effects_vol * .8f);
            Camera.main.GetComponent<ScreenShakeController>().StartShake(.5f, .1f);
            Debug.Log("THIS IS A TEST");
            StartCoroutine(explosion_anim());
            yield return new WaitForSeconds(.4f);
            Debug.Log("THIS IS A TEST");
            gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale = .0001f;
            Debug.Log("THIS IS A TEST");
            yield return new WaitForSeconds(.2f);
            Debug.Log("THIS IS A TEST");
            Destroy(gameObject.transform.parent.gameObject);
        }

    }

    private IEnumerator explosion_anim(){
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
    private IEnumerator beep(){
        while(true){
            distance = Vector2.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
            if (distance < 6){
                AudioSource.PlayClipAtPoint(beeping, gameObject.transform.position, GameControl.sound_effects_vol * .8f);
                yield return new WaitForSeconds(distance/10f);
            }
            yield return null;
        }
    }


}
