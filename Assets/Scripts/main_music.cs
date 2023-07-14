using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_music : MonoBehaviour
{
    public AudioClip background_music;
    public AudioClip lava_music;
    public AudioClip water_music;
    public AudioClip boss_music;
    public static main_music instance;
    bool music_changing = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Destroy(gameObject);
        }
        gameObject.GetComponent<AudioSource>().clip = background_music;
        gameObject.GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        if (GameObject.Find("Player") != null){
            if (GameObject.Find("Player").transform.position.y < -136 && GameObject.Find("Player").transform.position.y > -158 && gameObject.GetComponent<AudioSource>().clip != lava_music && !music_changing){
                music_changing = true;
                StartCoroutine(change_music(lava_music));
            }
            else if (((GameObject.Find("Player").transform.position.y < -74 && GameObject.Find("Player").transform.position.y > -136) || (GameObject.Find("Player").transform.position.y < -178)) && gameObject.GetComponent<AudioSource>().clip != water_music && !music_changing){
                music_changing = true;
                StartCoroutine(change_music(water_music));
            }
            else if (GameObject.Find("Player").transform.position.y > -74 && gameObject.GetComponent<AudioSource>().clip != background_music && !music_changing){
                music_changing = true;
                StartCoroutine(change_music(background_music));
            }
            else if (GameObject.Find("Player").transform.position.y < -158 && GameObject.Find("Player").transform.position.y > -178 && gameObject.GetComponent<AudioSource>().clip != boss_music && !music_changing){
                music_changing = true;
                StartCoroutine(change_music(boss_music));
            }

        }
    }
    private IEnumerator change_music(AudioClip next){
        float initial_time = Time.time;
        float progress = 0f;
        float duration_sec = 1.5f;
        float orig_vol = gameObject.GetComponent<AudioSource>().volume;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            gameObject.GetComponent<AudioSource>().volume = Mathf.Lerp(orig_vol, 0, progress);
            yield return null;
        }
        gameObject.GetComponent<AudioSource>().clip = next;
        gameObject.GetComponent<AudioSource>().Play();
        initial_time = Time.time;
        progress = 0f;
        duration_sec = 1.5f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            gameObject.GetComponent<AudioSource>().volume = Mathf.Lerp(0, orig_vol, progress);
            yield return null;
        }
        music_changing = false;
    }
}
