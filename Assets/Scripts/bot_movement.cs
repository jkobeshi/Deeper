using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bot_movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movespeed = 2f;
    public float maxSpeed = 2f;
    float jumpHeight = 1.5f;
    public bool canJump = true;
    public bool resMov = false;
    public bool outofsight = true;
    public string direction = "";
    public Vector3 normalized_direction;
    private float dir_horizontal;
    private float dir_vertical;

    int SolidLayer = 1 << 3;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        outofsight = true;
    }

    void Update()
    {
        normalized_direction = Vector3.Normalize(GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position);
        dir_horizontal = normalized_direction.x;
        dir_vertical = normalized_direction.y;
        if (Mathf.Abs(dir_horizontal) == Mathf.Max(Mathf.Abs(dir_horizontal), Mathf.Abs(dir_vertical))){
            dir_vertical = 0;
        }
        else{
            dir_horizontal = 0;
        }
        RaycastHit2D drHit = Physics2D.Raycast(transform.position + new Vector3(0.465f, 0, 0), Vector2.down, 0.51f, SolidLayer);
        RaycastHit2D dlHit = Physics2D.Raycast(transform.position + new Vector3(-0.465f, 0, 0), Vector2.down, 0.51f, SolidLayer);
        RaycastHit2D urHit = Physics2D.Raycast(transform.position + new Vector3(0.465f, 0, 0), Vector2.up, 1f, SolidLayer);
        RaycastHit2D ulHit = Physics2D.Raycast(transform.position + new Vector3(-0.465f, 0, 0), Vector2.up, 1f, SolidLayer);
        if ((drHit.collider != null || dlHit.collider != null) && (urHit.collider == null && ulHit.collider == null)){
            canJump = true;
        }
        else{
            canJump = false;
        }
        if (!resMov && !outofsight){
            Moving();
        }
    }

    void Moving()
    {
        Vector2 vel = rb.velocity;
        RaycastHit2D tHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.465f, 0), new Vector3(dir_horizontal, 0, 0), 0.51f, SolidLayer);
        RaycastHit2D dHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.465f, 0), new Vector3(dir_horizontal, 0, 0), 0.51f, SolidLayer);
        RaycastHit2D mHit = Physics2D.Raycast(transform.position, new Vector3(dir_horizontal, 0, 0), 0.51f, SolidLayer);
        if (dir_horizontal > 0){
            direction = "east";
        }
        else if (dir_horizontal < 0){
            direction = "west";
        }
        if (dir_vertical > 0){
            direction = "north";
        }
        else if (dir_vertical < 0){
            direction = "south";
        }
        if (GetComponentInChildren<shock_anim>().frozen){
            rb.velocity = Vector2.zero;
        }
        else{
        if ((dir_horizontal != 0) && (tHit.collider == null) && (mHit.collider == null) && (dHit.collider == null))
            vel.x = dir_horizontal * movespeed;
        if ((dir_vertical > 0)  && canJump && (dir_horizontal == 0))
        { canJump = false; vel.y = movespeed * jumpHeight; }
        rb.velocity = vel;
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.name == "Player" && !GameControl.instance.gameOver && !GetComponentInChildren<shock_anim>().frozen && inventory.instance != null){
            GameControl.instance.HandleDeathAndReset("Corrupted Miner");
        }
    }
}

