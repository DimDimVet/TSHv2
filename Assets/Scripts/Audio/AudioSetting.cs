using System;
using UnityEngine;

namespace AudioScene
{
    [CreateAssetMenu(fileName = "AudioSetting", menuName = "ScriptableObjects/AudioSetting")]
    public class AudioSetting : ScriptableObject
    {

        [Header("Уровень музыки"), Range(0, 1)]
        public float MuzVol = 0.5f;
        [Header("Уровень эффектов"), Range(0, 1)]
        public float EfectVol = 0.5f;

        [Header("Звуковой файл - кнопка")]
        public AudioClip AudioClipButton;

        [Header("Звуковой файл - фон")]
        public AudioClip AudioClipGnd;

        public void SetAudioParametr(float muzVol, float efectVol)
        {
            PlayerPrefs.SetFloat("CurrentMuzVol", muzVol);
            PlayerPrefs.SetFloat("CurrentEfectVol", efectVol);
            MuzVol = muzVol;
            EfectVol = efectVol;
        }
        public void GetAudioParametr()
        {
            MuzVol = PlayerPrefs.GetFloat("CurrentMuzVol");
            EfectVol = PlayerPrefs.GetFloat("CurrentEfectVol");
        }
    }
}

