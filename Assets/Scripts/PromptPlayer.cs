using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptPlayer : MonoBehaviour
{
    [SerializeField] Sprite DetectedSprite;
    [SerializeField] SpriteRenderer childRenderer;
    [SerializeField] SpriteRenderer childPromptRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.childCount > 1) {
            childRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            childPromptRenderer = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else {
            Debug.Log("No children!");
        }
        childRenderer.enabled = false;
        childPromptRenderer.enabled = false;
    }

    public void showPrompt() {
        childRenderer.enabled = true;
        childPromptRenderer.enabled = true;
    }

    public void destroyPrompt() {
        childRenderer.enabled = false;
        childPromptRenderer.enabled = false;
    }
}
