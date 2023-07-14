using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_beam_control : MonoBehaviour
{
    [SerializeField] float LightDuration = 2f;
    public AudioClip power_down;
    public AudioClip explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObjectOverTime(10, 45, .5f));
    }

    public void UpgradeLightDuration(float delta) {
        LightDuration += delta;
    }

    // Update is called once per frame
    IEnumerator MoveObjectOverTime(float initial_ang, float dest_ang, float duration_sec)
    {
        //AudioSource.PlayClipAtPoint(power_down, Camera.main.transform.position, .5f * GameControl.sound_effects_vol);
        GameObject.Find("Player").GetComponentInChildren<Light>().intensity = 0;
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            gameObject.GetComponent<Light>().spotAngle = Mathf.Lerp(initial_ang, dest_ang, progress);
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color ( Mathf.Lerp(1, 0, progress),  Mathf.Lerp(1, 0, progress), 1, 1);
            yield return null;
        }
        AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position, GameControl.sound_effects_vol * .8f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);
        gameObject.GetComponentInChildren<jolt>().call_jolter();
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(LightDuration);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1);
        GameObject.Find("Player").GetComponentInChildren<Light>().intensity = 30;
        Destroy(gameObject);
    }
}
