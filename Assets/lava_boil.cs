using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava_boil : MonoBehaviour
{
    // Start is called before the first frame update
    private bool boiling = false;
    private Sprite orig_sprite;
    public GameObject light;
    public float duration_sec;
    public List<Sprite> sprites = new List<Sprite>();
    void Start()
    {
        orig_sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(flipper());
    }

    private IEnumerator flipper(){
        while(true){
            if (gameObject.GetComponent<SpriteRenderer>().flipX == false){
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else{
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            yield return new WaitForSeconds(.3f);
        }
    }
    // Update is called once per frame
    public IEnumerator boil(){
        Debug.Log("here!");
        if (boiling == false)
        {
        boiling = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        float initial_time = Time.time;
        GameObject clone = GameObject.Instantiate(light, gameObject.transform.position + new Vector3(0, 0, -.75f), Quaternion.identity);
        float progress = 0f;
            while (progress < 1.0f)
            {
                progress = (Time.time - initial_time) / duration_sec;
                yield return null;
            }
        gameObject.GetComponent<SpriteRenderer>().sprite = orig_sprite;
        //gameObject.GetComponentInChildren<Light>().intensity = .5f;
        Destroy(clone);
        boiling = false;
        }
    }
}
