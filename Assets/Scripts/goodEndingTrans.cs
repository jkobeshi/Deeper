using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goodEndingTrans : MonoBehaviour
{
    private GameObject black_bg;
    public string scene_to_transition_to;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("Shop Menu"));
        Destroy(GameObject.Find("Ending Choice"));
        black_bg = Camera.main.gameObject.GetComponent<start_black_bg>().black_bg;
        StartCoroutine(transition());
    }

    // Update is called once per frame
    private IEnumerator transition(){
        black_bg.SetActive(true);
        float duration_sec = 2f;
        float initial_time = Time.time;
        float progress = 0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            black_bg.GetComponent<Image>().color = new Color (0, 0, 0, progress);
            yield return null;
        }
        GameControl.instance.Reloading = true;
        GameControl.batteries_to_respawn.Clear();
        GameControl.mines_to_respawn.Clear();
        SceneManager.LoadScene(scene_to_transition_to);
    }
}
