using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Booster : MonoBehaviour
{
    public GameObject flameGO;
    private AudioSource flame_noise;
    public GameObject flame;
    float maxSpeed = 4f;
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    bot_movement bot_move;
    public GameObject mainCam;
    // Start is called before the first frame update
    void Start()
    {
        flame_noise = flameGO.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "Player") {
            playerMovement = gameObject.GetComponent<PlayerMovement>();
        }
        else {
            bot_move = gameObject.GetComponent<bot_movement>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.name == "Player") {
            if (!PlayerMovement.instance.resMov && (Input.GetKey("up") || Input.GetKey("w")) && rb.position.y < 0) {
                rb.AddForce(new Vector2(0, 15));
            }
        }
        else {
            if (!gameObject.GetComponent<bot_movement>().outofsight && gameObject.GetComponent<bot_movement>().direction == "north" && rb.position.y < 0) {
                rb.AddForce(new Vector2(0, 10f));
            }
        }
        if (rb.velocity.y > maxSpeed) {
            rb.velocity = rb.velocity.normalized*maxSpeed;
        }
    }
    private void Update()
    {
        if (gameObject.name == "Player") {
            if ((Input.GetKey("up") || Input.GetKey("w")) && !PlayerMovement.instance.resMov || SceneManager.GetActiveScene().name == "Moon")
            {
                flame.SetActive(true);
                flame_noise.volume = 0.06f * GameControl.sound_effects_vol;
                mainCam.GetComponent<ScreenShakeController>().StartShake(0.5f, 0.03f);
            }
            else
            {
                flame.SetActive(false);
                flame_noise.volume = 0.0f;
            }
        }
        else {
            if (gameObject.GetComponent<bot_movement>().direction == "north" && !gameObject.GetComponent<bot_movement>().outofsight){
            flame.SetActive(true);
            flame_noise.volume = 0.06f * GameControl.sound_effects_vol;
            }
            else{
            flame.SetActive(false);
            flame_noise.volume = 0.0f;
            }
        }
    }
}
