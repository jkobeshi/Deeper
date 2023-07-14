using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 lastPos;
    bool BeginFollow = false;
    public float transitionSpeed = 0.2f;
    public Vector3 offset;
    [SerializeField] alter_intensity GoalLightController;
    [SerializeField] Canvas GameUI;
    private void Start()
    {
        GameControl.instance.Reloading = false;
        GameControl.instance.shield_health = 0;
        GameControl.instance.shield_max = 0;
        if (inventory.instance != null){
        PlayerMovement.instance.resMov = true;
        GameUI = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameUI.enabled = false;
        GoalLightController = GameObject.Find("Environmental Blocks/Diamond/Diamond Light").GetComponent<alter_intensity>();
        GoalLightController.enabled = false;
        BeginFollow = false;
        StartCoroutine(ShowDiamond());
        }
        else{
            BeginFollow = true;
        }
    }
    IEnumerator ShowDiamond()
    {
        yield return new WaitForSeconds(4f);
        BeginFollow = true;
        StartCoroutine(SlowTransition());
    }
    IEnumerator SlowTransition() {
        transitionSpeed = 0.02f;
        yield return new WaitForSeconds(6f);
        transitionSpeed = 0.2f;
        GameUI.enabled = true;
        GoalLightController.enabled = true;
        PlayerMovement.instance.resMov = false;
    }
    private void Update()
    {
        if (BeginFollow) {
            if (player != null)
            {
                lastPos = player.position;
            }
            Vector3 playerPosition = lastPos + offset; playerPosition.z = -10;
            Vector3 Transition = Vector3.Lerp(transform.position, playerPosition, transitionSpeed * Time.deltaTime * 60);
            transform.position = Transition;
        }

        if(player.position.y < -158f && player.position.y > -178) {
            GetComponent<Camera>().orthographicSize = 10f;
            offset = new Vector3(0f, -2f, 0f);
        } else{
            GetComponent<Camera>().orthographicSize = 5f;
            offset = new Vector3(0f, 0f, 0f);
        }
    }
}
