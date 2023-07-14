using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_bomb_control : MonoBehaviour
{
    private string dir_to_go = "";
    public float duration_sec = 1;
    public float duration_sec_explode = .5f;
    [SerializeField] float LightDuration = 4f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bomb_movement());
    }

    public void UpgradeLightDuration(float delta) {
        LightDuration += delta;
    }

    private IEnumerator bomb_movement(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 normMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        Vector3 dest_pos = new Vector3(normMousePos.x, normMousePos.y, -9);
        float initial_time = Time.time;
        Vector3 initial_position = gameObject.transform.position;
        float progress = (Time.time - initial_time) / duration_sec;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            gameObject.transform.position = Vector3.Lerp(initial_position, dest_pos, progress);
            yield return null;
        }
        float initial_time_explode = Time.time;
        float initial_ang = 10.5f;
        float dest_ang = 50f;
        float progress_explode = (Time.time - initial_time_explode) / duration_sec_explode;
        while (progress_explode < 1.0f){
            progress_explode = (Time.time - initial_time_explode) / duration_sec_explode;
            gameObject.GetComponent<Light>().spotAngle = Mathf.Lerp(initial_ang, dest_ang, progress_explode);
            yield return null;
        }
        yield return new WaitForSeconds(LightDuration);
        Destroy(gameObject);
    }
}
