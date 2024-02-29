using UnityEngine;

namespace UI
{
    public class SettingsUIMenu : SettingsUI
    {
        protected override void RunAudio()
        {
            panels.AudioSet();

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = winAudioSetting.AudioClipButton;
            audioSource.volume = winAudioSetting.EfectVol;

            audioSourceMuz = gameObject.AddComponent<AudioSource>();
            audioSourceMuz.clip = winAudioSetting.AudioClipGnd;
            audioSourceMuz.volume = winAudioSetting.MuzVol;
            audioSourceMuz.loop = true;
        }
    }
}

