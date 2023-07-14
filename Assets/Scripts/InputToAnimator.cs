using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;
    public static bool specialAnim = false;
    private enum direction{
        left,
        right,
        up,
        down
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKey("up") || Input.GetKey("w")) {
            animator.SetInteger("Direction", (int)direction.up);
        }
        else{
            if(Input.GetKey("left") || Input.GetKey("a")) {
                animator.SetInteger("Direction", (int)direction.left);
            }
            if(Input.GetKey("right") || Input.GetKey("d")) {
                animator.SetInteger("Direction", (int)direction.right);
            }
            if(Input.GetKey("down") || Input.GetKey("s")) {
                animator.SetInteger("Direction", (int)direction.down);
            }
        }
        if (!PlayerMovement.instance.resMov)
        {
            animator.speed = 1.0f;
            /*animator.SetFloat("horizontal_input", Input.GetAxisRaw("Horizontal"));
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                animator.SetFloat("vertical_input", Input.GetAxisRaw("Vertical"));
            }*/
        }
        else
        {
            animator.speed = 0.0f;
        }
    }
}
