using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.K)) {
        //     StartShake(.5f, 0.1f);
        // }
    }

    private void LateUpdate() {
        if(shakeTimeRemaining > 0) {
            shakeTimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f)*shakePower;
            float yAmount = Random.Range(-1f, 1f)*shakePower;
            transform.position += new Vector3(xAmount, yAmount, 0f);
        }
    }

    public void StartShake(float length, float power) {
        power *= GameControl.screen_shake_mult;
        shakeTimeRemaining = length;
        shakePower = power;
    }
}
