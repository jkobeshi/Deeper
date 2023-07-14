using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveMovement : MonoBehaviour
{
    private float ref_x;
    private float ref_y;
    public float index = 0;
    Rigidbody2D rb;
    Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ref_x = transform.parent.GetComponent<Transform>().position.x;
        ref_y = transform.parent.GetComponent<Transform>().position.y;
        if(index == 0) {
            ref_y = ref_y + 0.5f;
        }
        double newpos = Math.Sin(Time.time*3 + index/2)/2 + ref_x;
        double newrot = Math.Sin(Time.time*3 + index/2 + 90)*(180/3.14)/8;

        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = (float)newrot;  //this number is the degree of rotation around Z Axis
        tf.rotation = Quaternion.Euler(rotationVector);

        rb.position = new Vector2((float)newpos, ref_y - 0.4f*(float)index);
    }
}
