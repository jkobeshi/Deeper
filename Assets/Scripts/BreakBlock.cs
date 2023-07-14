using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private int key_dir = 0;
    private float initial_time;
    private float curr_time;
    float miningDur = 0.25f;
    private Transform player_tf;
    bool moving = false;
    Vector2 dir = Vector2.zero;
    public AudioClip drill_sound;
    public AudioClip bad_drill;
    public GameObject mainCam;
    private float timePlayed;
    // Start is called before the first frame update
    void Start()
    {
        player_tf = GetComponent<Transform>();
        timePlayed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player") {
            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else {
            dir = gameObject.GetComponent<bot_movement>().normalized_direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        initial_time = Time.time;
    }

    void FixedUpdate() {
        curr_time = Time.time;
        RaycastHit2D hit = Physics2D.Raycast(player_tf.position, dir, 0.7f);
        // If the collided object doesn't have this component, it's not a minable block
        // and we don't care about it
        if (hit.collider != null && hit.collider.gameObject.GetComponent<IsBreakable>() != null) {
            if (((curr_time - initial_time) > miningDur) && (dir != Vector2.zero)) {
                if (inventory.instance == null){
                        if (gameObject.tag == "Player" && !moving && !PlayerMovement.instance.resMov && hit.collider.gameObject.GetComponent<IsBreakable>().hardness == 1) {
                        AudioSource.PlayClipAtPoint(drill_sound, Camera.main.transform.position, .1f * GameControl.sound_effects_vol);
                        StartCoroutine(MoveObjectOverTime(player_tf, player_tf.position, hit.transform.position, 
                            hit.collider.gameObject.GetComponent<IsBreakable>().miningTime, hit.transform.gameObject));
                        }
                }
                else{
                if (inventory.instance.hardness >= hit.collider.gameObject.GetComponent<IsBreakable>().hardness && !moving && !PlayerMovement.instance.resMov) {
                    initial_time = Time.time;
                    if (gameObject.tag == "Player") {
                        AudioSource.PlayClipAtPoint(drill_sound, Camera.main.transform.position, .1f * GameControl.sound_effects_vol);
                        StartCoroutine(MoveObjectOverTime(player_tf, player_tf.position, hit.transform.position, 
                            hit.collider.gameObject.GetComponent<IsBreakable>().miningTime, hit.transform.gameObject));
                    }
                    else if (!gameObject.GetComponent<bot_movement>().outofsight) {
                        AudioSource.PlayClipAtPoint(drill_sound, Camera.main.transform.position, .1f * GameControl.sound_effects_vol);
                        StartCoroutine(MoveObjectOverTime(player_tf, player_tf.position, hit.transform.position, 1f, hit.transform.gameObject));
                    }
                }
                else if (inventory.instance.hardness < hit.collider.gameObject.GetComponent<IsBreakable>().hardness && gameObject.tag == "Player") {
                    if (!PlayerMovement.instance.resMov && !PlayerMovement.instance.shop_active) {
                        mainCam.GetComponent<ScreenShakeController>().StartShake(0.5f, 0.05f);
                        if(Time.time - timePlayed > 0.75f) {
                            timePlayed = Time.time;
                            AudioSource.PlayClipAtPoint(bad_drill, Camera.main.transform.position, .1f * GameControl.sound_effects_vol);
                        }
                        if(!hit.collider.gameObject.GetComponent<ParticleSystem>().isPlaying) {
                            hit.collider.gameObject.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                }
            }
        }

    }
    IEnumerator MoveObjectOverTime(Transform target1, Vector3 initial_pos1, Vector3 dest_pos1, float duration_sec, GameObject temp)
    {
        moving = true;
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;

        if (Mathf.Abs(dest_pos1.x - initial_pos1.x) > 0){
            dest_pos1.y = temp.GetComponent<Transform>().position.y;
        }
        else
        {
            dest_pos1.x = temp.GetComponent<Transform>().position.x;
        }
        SpriteRenderer rend = temp.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            Vector3 new_position1 = Vector3.Lerp(initial_pos1, dest_pos1, progress);
            if (rend != null){
                rend.color = new Color(1f, 1f, 1f, progress);
            }
            target1.position = new_position1;
            yield return null;
        }
        target1.position = dest_pos1;
        if (gameObject.tag == "Player" && temp != null){
            temp.GetComponent<IsBreakable>().handleBreak(true);
        }
        else if (temp != null){
            temp.GetComponent<IsBreakable>().handleBreak(false);
        }
        moving = false;
    }
}
