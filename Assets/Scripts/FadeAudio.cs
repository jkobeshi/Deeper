using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadeAudio : MonoBehaviour
{
    public bool fading_in = false;
    public bool must_fade_out = false;

    void Update() {

    }
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        if(!fading_in) {
            fading_in = true;
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            fading_in = false;
            yield break;
        } else {
            yield break;
        }
    }
}