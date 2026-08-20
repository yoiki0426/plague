using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_Button_alpha : MonoBehaviour
{
    public Image image;
    public float threshold = 0.5f;

    void Start()
    {
        image= gameObject.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
