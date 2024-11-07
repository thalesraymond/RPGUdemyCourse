using System;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameUI
{
    public class VolumeSliderUI : MonoBehaviour
    {
        [SerializeField] public Slider slider;
        [SerializeField] public string parameter;
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField] private float multiplier;

        public void SliderValue(float value) => audioMixer.SetFloat(parameter, Mathf.Log10(value) * multiplier);

        public void LoadSlider(float value)
        {
            if (value < this.slider.minValue) return;
            
            this.slider.value = value;
        }
    }
}
