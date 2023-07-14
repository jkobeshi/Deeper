using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_black_bg : MonoBehaviour
{
    public GameObject black_bg;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade_in());
    }

    private IEnumerator fade_in()
    {
        float duration_sec = 2f;
        float initial_time = Time.time;
        float progress = 0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            black_bg.GetComponent<Image>().color = new Color(0, 0, 0, 1 - progress);
            yield return null;
        }
        black_bg.SetActive(false);
    }
    // Update is called once per frame

}
