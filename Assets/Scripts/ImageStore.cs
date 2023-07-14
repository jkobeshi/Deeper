using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ImageStore : MonoBehaviour
{
    [SerializeField] Sprite icon;
    [SerializeField] VideoClip tutorial_vid;


    public Sprite getImage() {
        return icon;
    }

    public VideoClip getVideo() {
        return tutorial_vid;
    }
}
