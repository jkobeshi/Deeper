using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_terrain : MonoBehaviour
{
    public List<GameObject> terrain;
    private GameObject chosen_object;
    private GameObject chosen_object_copy;
    // Start is called before the first frame update
    void Start()
    {
        chosen_object = terrain[Random.Range(0, terrain.Count)];
        chosen_object_copy = GameObject.Instantiate(chosen_object, gameObject.transform);
        chosen_object_copy.transform.position += new Vector3(0, 1, 0);

    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameObject.Destroy(chosen_object_copy);
    }
}
