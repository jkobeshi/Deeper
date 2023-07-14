using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GradOpac : MonoBehaviour
{
    Image opac;
    float holder;
    void Start()
    {
        opac = GetComponent<Image>();
        holder = opac.color.g;
    }

    IEnumerator GraduOpac()
    {
        while (holder > 0)
        {
            opac.color -= new Color(0, 0, 0, 0.1f);
            holder -= 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.SetActive(false);
    }
}
