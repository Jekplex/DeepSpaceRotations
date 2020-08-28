using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Reference
// https://www.youtube.com/watch?v=xNHSGMKtlv4

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private Slider thisSlider;

    private void Start()
    {
        thisSlider = GetComponent<Slider>();
        thisSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);

        PlayerPrefs.SetFloat("Volume", sliderValue);
    }
}
