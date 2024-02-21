using UnityEngine;

namespace UI
{
    public struct WinAudioSetting
    {
        public float MuzVol;
        public float EfectVol;
        public AudioClip AudioClipButton;
        public AudioClip AudioClipGnd;
        //
        public int MinWidth;
        public int MinHeight;
    }
    public class UIPanelsExecutor : IUIPanelsExecutor
    {
        private WinAudioSetting winAudioSetting;
        public void SetWinAudio(WinAudioSetting _winAudioSetting)
        {
            winAudioSetting = new WinAudioSetting();
            winAudioSetting = _winAudioSetting;
        }
        public void CallGndPanel()
        {
        }
        public void CallButtonPanel()
        {
        }
        public void CallSettPanel()
        {
        }
        public void CallRezultPanel()
        {
        }
    }
}

