using StatisticPlayer;
using System;
using System.Collections.Generic;

namespace UI
{
    public interface IUIPanelsExecutor
    {
        void CallButtonPanel();
        void CallGndPanel();
        void CallRezultPanel();
        void CallSettPanel();
        void Set(WinAudioSetting _winAudioSetting, PanelsLvl _panelsLvl);
        Action<ActivPanel> OnStateUI { get; set; }
        void ScreenSet();
        List<string> TextScreen { get; }
        int IndexCurrentScreen { get; }
        void SetNewResolution(int indexDrop);
        void AudioSet();
        void SetNewAudio(WinAudioSetting _winAudioSetting);
        Action<WinAudioSetting> OnParametrUI { get; set; }
        Action<Statistic> OnStatisticUI { get; set; }
    }
}