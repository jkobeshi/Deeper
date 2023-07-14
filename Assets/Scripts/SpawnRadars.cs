using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRadars : MonoBehaviour
{
    [SerializeField] GameObject radar;
    [SerializeField] float max_x;
    [SerializeField] float max_y;
    [SerializeField] float spawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginSpawningRadars());
    }

    IEnumerator BeginSpawningRadars() {
        while (true) {
            Instantiate(radar, new Vector3(Random.Range(-max_x, max_x), Random.Range(-max_y, max_y), 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
