using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateDrill : MonoBehaviour
{
    public GameObject Pivot;
    PlayerMovement movement;
    bot_movement bot_move;
    float desiredDir = 0;
    float currRot = 0;
    float spd = 2;
    public SpriteRenderer beam_sprite;
    public bool lasering;
    public bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        spd = spd * 4.5f;
        if (GetComponent<PlayerMovement>() != null){
        movement = GetComponent<PlayerMovement>();
        }
        else{
            bot_move = GetComponent<bot_movement>();
        }
        StartCoroutine(MoveToDesired());
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != null){
            if(Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Vertical") == 0)
            {
                desiredDir = 0f;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Vertical") > 0)
            {
                desiredDir = 45f;
            }
            else if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") > 0)
            {
                desiredDir = 90f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Vertical") > 0)
            {
                desiredDir = 135f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Vertical") == 0)
            {
                desiredDir = 180f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Vertical") < 0)
            {
                desiredDir = 225f;
            }
            else if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") < 0)
            {
                desiredDir = 270f;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Vertical") < 0)
            {
                desiredDir = 315f;
            }
        }
        else{
            if(bot_move.direction == "east") {
                desiredDir = 0f;
            } else if(bot_move.direction == "north") {
                desiredDir = 90f;
            } else if(bot_move.direction == "west") {
                desiredDir = 180f;
            } else if(bot_move.direction == "south") {
                desiredDir = 270f;
            }
        }

    }

    IEnumerator MoveToDesired() {
        float rotDir = 1;
        while(true) {
            if(!locked) {
                currRot = currRot%360;
                if(currRot < 0) {
                    currRot = 360 + currRot;
                }
                if(desiredDir != currRot) {
                    if((desiredDir - currRot) > 0) {
                        if(Mathf.Abs(desiredDir - currRot) <= 180) {
                            rotDir = spd;
                        } else {
                            rotDir = -spd;
                        }
                    } else {
                        if(Mathf.Abs(desiredDir - currRot) <= 180) {
                            rotDir = -spd;
                        } else {
                            rotDir = spd;
                        }
                    }

                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.z = currRot + rotDir;  //this number is the degree of rotation around Z Axis
                    currRot = currRot + rotDir;
                    Pivot.transform.rotation = Quaternion.Euler(rotationVector);
                    }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
