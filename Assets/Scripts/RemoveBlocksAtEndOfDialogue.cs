using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlocksAtEndOfDialogue : MonoBehaviour
{
    [SerializeField] List<BoxCollider2D> CollidersToRemove = new List<BoxCollider2D>();
    [SerializeField] npc_dialogue DialogueComponent;

    private void Start() {
        DialogueComponent = gameObject.GetComponent<npc_dialogue>();
    }

    private void Update() {
        if (DialogueComponent.index == (DialogueComponent.dialogue.Count - 1)) {
            foreach (BoxCollider2D collider in CollidersToRemove) {
                collider.enabled = false;
            }
        }
    }
}
