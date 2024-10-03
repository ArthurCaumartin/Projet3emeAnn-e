using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Party : MonoBehaviour
{
    public float speed;
    public Image image;

    void Update()
    {
        image.color = Color.HSVToRGB(Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * speed)), 1, 1);
    }
}
