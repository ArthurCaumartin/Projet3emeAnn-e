using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider _slider;
    private TextMeshProUGUI _sliderText;

    private void Start()
    {
        _sliderText = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeValue()
    {
        int valuetoChange = Mathf.RoundToInt(_slider.value * 100);
        _sliderText.text = valuetoChange.ToString();
    }
}
