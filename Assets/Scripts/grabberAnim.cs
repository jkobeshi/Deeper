using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabberAnim : MonoBehaviour
{
    public AnimationCurve yCurve;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite sprit1;
    public Sprite sprit2;
    public Sprite sprit3;
    public AudioClip clip;
    private float timeElapsed = 0;
    private bool started = false;
    private bool currentlyAttacking = false;
    private Vector2 startPosition;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!started)
        {
            startPosition = transform.position;
        }
        else
        {
            timeElapsed += Time.deltaTime;

            rb.MovePosition(new Vector2(
                startPosition.x /*+ xCurve.Evaluate(timeElapsed)*/,
                startPosition.y + yCurve.Evaluate(timeElapsed))
                );
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !currentlyAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public IEnumerator Attack()
    {
        if (!gameObject.GetComponentInChildren<shock_anim>().frozen){
        started = true;
        currentlyAttacking = true;
        ChangeSprite(sprit1);
        yield return new WaitForSeconds(0.6f);
        ChangeSprite(sprit3);
        if (!gameObject.GetComponentInChildren<shock_anim>().frozen){
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        if (!gameObject.GetComponentInChildren<shock_anim>().frozen){
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, .5f * GameControl.sound_effects_vol);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        ChangeSprite(sprit2);
        }
        yield return new WaitForSeconds(1.4f);
        started = false;
        currentlyAttacking = false;
        timeElapsed = 0;
        }
    }
}
