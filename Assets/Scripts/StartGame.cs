using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] string MainSceneName;
    public static bool disabled = false;
    public GameObject black_bg;

    void Start(){
        disabled = false;
    }
    // Start is called before the first frame update
    public void LoadGame() {
        if (!disabled){
        StartCoroutine(fade_to_black());
        }
    }

    private IEnumerator fade_to_black(){
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
        SceneManager.LoadScene(MainSceneName);
    }
}
