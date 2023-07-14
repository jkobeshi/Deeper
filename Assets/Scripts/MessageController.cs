using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] float displayTime = 4f;
    [SerializeField] GameObject CanvasObject;
    [SerializeField] Text DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        CanvasObject.SetActive(false);
        DisplayText.text = "";
    }

    public void DisplayMessage(string messageToDisplay, Color textColor) {
        StartCoroutine(InternalDisplayMessage(messageToDisplay, textColor));
    }

    IEnumerator InternalDisplayMessage(string messageToDisplay, Color textColor) {
        CanvasObject.SetActive(true);
        DisplayText.text = messageToDisplay;
        DisplayText.color = textColor;
        yield return new WaitForSeconds(displayTime);
        DisplayText.text = "";
        CanvasObject.SetActive(false);
    }
}
