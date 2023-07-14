using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move_credits_up : MonoBehaviour
{
    // Start is called before the first frame update

    public IEnumerator Start_Credits(){
        while (true){
        gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().position = new Vector3(gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().position.x, gameObject.GetComponent<Canvas>().GetComponent<RectTransform>().position.y + 1.25f, 0);
        yield return new WaitForSeconds(.005f);
        }
    }
}
