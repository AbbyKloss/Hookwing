using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SaveSliderVol : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Slider slider;

    public void Start() {
        float outfloat;
        slider = GetComponent<Slider>();
        audioMixer.GetFloat("volume", out outfloat);
        slider.value = outfloat;
    }
}
