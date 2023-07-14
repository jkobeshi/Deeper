using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndPromptPlayer : MonoBehaviour
{
    [SerializeField] Sprite UndetectedSprite;
    [SerializeField] Sprite DetectedSprite;
    [SerializeField] SpriteRenderer childRenderer;
    [SerializeField] SpriteRenderer childPromptRenderer;
    [SerializeField] GameObject tutorialRenderer;
    private bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.childCount > 1) {
            childRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            tutorialRenderer = gameObject.transform.GetChild(1).gameObject;
            childPromptRenderer = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else {
            Debug.Log("No children!");
        }
        tutorialRenderer.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
            tutorialRenderer.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            childRenderer.sprite = DetectedSprite;
            childPromptRenderer.enabled = true;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            if (gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<npc_dialogue>() != null){
                if (gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<npc_dialogue>().dialogue_finished)
                {
                    childRenderer.sprite = null;
                }
                else{
                    childRenderer.sprite = UndetectedSprite;
                }
            }
            else{
                childRenderer.sprite = UndetectedSprite;
            }
            if (gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<npc_dialogue>() != null){
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<npc_dialogue>().index = 0;
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<npc_dialogue>().dialogue_text.text = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<npc_dialogue>().dialogue[0];
            }
            childPromptRenderer.enabled = false;
            tutorialRenderer.SetActive(false);
            playerInRange = false;

        }
    }
}
