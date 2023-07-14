using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAbove : MonoBehaviour
{
    public GameObject above_segment;
    Rigidbody2D above_rb;
    Rigidbody2D curr_rb;
    // Start is called before the first frame update
    void Start()
    {
        above_rb = above_segment.GetComponent<Rigidbody2D>();
        curr_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float diff = above_rb.position.x - curr_rb.position.x;
        if(curr_rb.position.x > 0.5f || curr_rb.position.x < -0.5f) {
            diff = diff - curr_rb.position.x;
        }
        curr_rb.AddForce(new Vector2(diff, 0));
    }
}
