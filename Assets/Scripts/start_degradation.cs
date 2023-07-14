using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_degradation : MonoBehaviour
{
    public GameObject moss;
    public GameObject drill;
    public GameObject end_credits;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PlayerMovement>().resMov = true;
        StartCoroutine(degrade());
    }

    // Update is called once per frame

    private IEnumerator degrade(){
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / 5f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / 10;
            gameObject.GetComponentInChildren<Light>().spotAngle = Mathf.Lerp(45f, 10f, progress);
            moss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0, 1, progress));
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        initial_time = Time.time;
        progress = (Time.time - initial_time) / 5f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / 5;
            gameObject.GetComponentInChildren<Light>().spotAngle = Mathf.Lerp(10f, 45f, progress);
            gameObject.GetComponentInChildren<Light>().color = new Color(1f, Mathf.Lerp(1, 0, progress), Mathf.Lerp(1, 0, progress), 1);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, Mathf.Lerp(1, 0, progress), Mathf.Lerp(1, 0, progress), 1);
            drill.GetComponent<SpriteRenderer>().color = new Color(1f, Mathf.Lerp(1, 0, progress), Mathf.Lerp(1, 0, progress), 1);
            moss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(1, 0, progress));
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<PlayerMovement>().resMov = false;
        StartCoroutine(end_credits.GetComponent<move_credits_up>().Start_Credits());
        yield return new WaitForSeconds(40f);
        Camera.main.gameObject.GetComponent<StartGame>().LoadGame();



    }
}
