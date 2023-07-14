using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    // Start is called before the first frame update
    public float parallax_mult;
    private float start_position;
    void Start()
    {
        start_position = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance_from_camera = Camera.main.transform.position.x * parallax_mult;
        transform.position = new Vector3(start_position + distance_from_camera, transform.position.y, transform.position.z);
    }
}
