using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alter_intensity : MonoBehaviour
{
    public float max_intensity;
    private Light battery_light;
    // Start is called before the first frame update
    void Start()
    {
        battery_light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        battery_light.intensity = (Mathf.Sin(Time.time)) * max_intensity;
    }
}
