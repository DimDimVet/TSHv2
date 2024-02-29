using Input;
using StatisticPlayer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IUIPanelsExecutor
    {
        void CallButtonPanel(bool isGnd = false);
        void CallGndPanel();
        void CallRezultPanel(bool isGnd = false, bool isRezultPanel = false);
        void CallSettPanel(bool isGnd = false);
        void CallGameMenu();
        void ExitGame();
        void CallOverScene();
        void CallVictoryScene();
        void Set(WinAudioSetting _winAudioSetting, PanelsLvl _panelsLvl, SceneIndex _sceneIndex);
        Action<ActivPanel> OnStateUI { get; set; }
        void ScreenSet(Resolution[] _resolutions);
        Action<Resolution> OnSetResolution { get; set; }
        List<string> TextScreen { get; }
        int IndexCurrentScreen { get; }
        void SetNewResolution(int indexDrop);
        void AudioSet();
        void AudioClick();
        void AudioMuz();
        Action<bool> OnAudioMuz { get; set; }
        void SetNewAudio(WinAudioSetting _winAudioSetting);
        Action<WinAudioSetting> OnParametrUI { get; set; }
        Action<bool> OnAudioClick { get; set; }
        Action<Statistic> OnStatisticUI { get; set; }
        void ReBootScene();
        void MainMenu();
        void ChargingSetParametr(Mode mode, int currentCountClip);
        Charging[] ChargingGetParametr();
        void CurrentMode(Mode currentMode);
        Action<Mode> OnCurrentMode { get; set; }
        void ChargingUpdate(Mode mode, bool isClipReLoad, int currentCountClip);
        Action<Charging> OnChargingUpdate { get; set; }
        void SelectCursor(bool isSelect);
        Action<bool> OnSelectCursor { get; set; }
    }
}