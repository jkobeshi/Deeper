using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingLight : MonoBehaviour
{
    // Start is called before the first frame update
    private Light pointLight;
    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] float fadeOutTime = 2f;
    [SerializeField] float maxIntensity = 2f;
    private float startTime;
    void Start()
    {
        pointLight = gameObject.GetComponent<Light>();
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut() {
        yield return new WaitForSeconds(0.5f);
        float lightIntensity = 0;
        pointLight.intensity = lightIntensity;
        float startTime = Time.time;
        float elapsedTime = 0;
        float elapsedProportion = 0;
        while (lightIntensity < maxIntensity) {
            elapsedTime = (Time.time - startTime);
            elapsedProportion = elapsedTime / fadeInTime;
            if (elapsedProportion > 1) {
                elapsedProportion = 1;
            }
            lightIntensity = Mathf.Lerp(0, maxIntensity, elapsedProportion);
            pointLight.intensity = lightIntensity;
            yield return null;
        }
        // Reset start time to measure fadeout time separately
        startTime = Time.time;
        while (lightIntensity > 0) {
            elapsedTime = (Time.time - startTime);
            elapsedProportion = elapsedTime / fadeOutTime;
            if (elapsedProportion > 1) {
                elapsedProportion = 1;
            }
            lightIntensity = Mathf.Lerp(maxIntensity, 0, elapsedProportion);
            pointLight.intensity = lightIntensity;
            yield return null;
        }
        Destroy(gameObject);
    }
}
