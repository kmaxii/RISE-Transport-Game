using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenImage : MonoBehaviour
{

    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        // Check if the screen resolution has changed, and if so, resize the image.
        if (Math.Abs(Screen.width - _rawImage.rectTransform.sizeDelta.x) > 0.1f || Math.Abs(Screen.height - _rawImage.rectTransform.sizeDelta.y) > 0.1f)
        {
            ResizeImage();
        }
    }

    private void ResizeImage()
    {
        // Set the Raw Image's size to match the screen resolution.
        _rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
