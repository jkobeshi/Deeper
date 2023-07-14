using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInButton : MonoBehaviour
{
    [SerializeField] Image ButtonImage;
    [SerializeField] Button Button;
    [SerializeField] Text Text;
    [SerializeField] float fadeInTime;
    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("Title Screen Canvas/Play Game Button").GetComponent<Button>();
        ButtonImage = GameObject.Find("Title Screen Canvas/Play Game Button").GetComponent<Image>();
        Text = GameObject.Find("Title Screen Canvas/Play Game Button/Text").GetComponent<Text>();
        ButtonImage.enabled = false;
        // Button.enabled = false;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    IEnumerator FadeIn() {
        yield return new WaitForSeconds(2f);
        ButtonImage.enabled = true;

        Color32 lerpToColorButton = new Color32(190, 20, 36, 255);
        Color32 lerpFromColorButton = new Color32(190, 20, 36, 0);
        Color32 lerpToColorText = Color.black;
        Color32 lerpFromColorText = new Color32(0, 0, 0, 0);

        ButtonImage.color = lerpFromColorButton;

        for (float a_value = 0f; a_value < fadeInTime; a_value += Time.deltaTime) {

            float normalizedTime = a_value/fadeInTime;
            float newAlpha = 255 * normalizedTime;
            Color32 tempColor = ButtonImage.color;
            tempColor.a = (byte)newAlpha;
            ButtonImage.color = tempColor;
            // ButtonImage.color = Color.Lerp(lerpFromColorButton, lerpToColorButton, normalizedTime);
            // Debug.Log(lerpFromColorButton);
            // Debug.Log(lerpToColorButton);
            // Debug.Log(ButtonImage.color);
            Text.color = Color.Lerp(lerpFromColorText, lerpToColorText, normalizedTime);
            yield return null;
        }
        ButtonImage.color = lerpToColorButton;
        Text.color = lerpToColorText;

        Button.enabled = true;
    }


}
