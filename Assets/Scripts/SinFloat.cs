using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFloat : MonoBehaviour
{
    [SerializeField] float baseY;
    [SerializeField] float baseX;
    [SerializeField] bool horizontal = false;
    [SerializeField] float sinMultiplier = 1;
    [SerializeField] int periodOffset = 0;

    void Start() {
        baseY = gameObject.transform.position.y;
        baseX = gameObject.transform.position.x;
    }

    void FixedUpdate() {
        if (horizontal) {
            Vector3 tempPosition = gameObject.transform.position;
            tempPosition.x = baseX + (Mathf.Sin(Time.time + (periodOffset * (Mathf.PI / 2))) * sinMultiplier);
            gameObject.transform.position = tempPosition;
        }
        else {
            Vector3 tempPosition = gameObject.transform.position;
            tempPosition.y = baseY + (Mathf.Sin(Time.time + (periodOffset * (Mathf.PI / 2))) * sinMultiplier);
            gameObject.transform.position = tempPosition;
        }
    }


}
