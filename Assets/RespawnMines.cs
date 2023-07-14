using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMines : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject minePrefab;
    // Update is called once per frame
    void Update()
    {
        foreach(var spawner in spawners) {
            if(spawner.GetComponent<CheckMine>().mine == null) {
                StartCoroutine(RespawnSpawner(spawner));
            }
        }
    }

    IEnumerator RespawnSpawner(GameObject spawner) {
        spawner.GetComponent<CheckMine>().mine = Instantiate(minePrefab, new Vector2(-100f, -100f), Quaternion.identity);
        yield return new WaitForSeconds(2);
        spawner.GetComponent<CheckMine>().mine.transform.position = spawner.transform.position;
    }
}
