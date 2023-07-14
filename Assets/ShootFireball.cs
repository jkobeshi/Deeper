using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    public GameObject fireball;
    public GameObject[] spawners;
    bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var spawner in spawners)
        {
            StartCoroutine(ShootBalls(spawner));
        }
        // StartCoroutine(TempWait());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootBalls(GameObject spawner) {
        while(true) {
            if(!waiting) {
                float randomNum = Random.Range(1f, 3f);
                Instantiate(fireball, spawner.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(randomNum, 5f);    
                Instantiate(fireball, spawner.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-randomNum, 5f);    
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
    }

    IEnumerator TempWait() {
        while(true) {
            waiting = false;
            yield return new WaitForSeconds(Random.Range(4f, 6f));
            waiting = true;
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
}
