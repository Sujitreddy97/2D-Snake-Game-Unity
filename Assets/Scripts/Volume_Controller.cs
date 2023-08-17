using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume_Controller : MonoBehaviour
{
    [SerializeField] private Slider BG_Slider;
    [SerializeField] private Slider SFX_Slider;

    private void Start()
    {
        BG_Slider.onValueChanged.AddListener(OnBGSliderValueChange);
        SFX_Slider.onValueChanged.AddListener(OnSFXSliderValueChange);
    }

    private void OnBGSliderValueChange(float value)
    {
        Audio_Manager.instance.SetBGVolume(value);
    }

    private void OnSFXSliderValueChange(float value)
    {
        Audio_Manager.instance.SetBGVolume(value);
    }
}
