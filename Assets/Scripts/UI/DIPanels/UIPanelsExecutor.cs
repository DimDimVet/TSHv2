using AudioScene;
using Input;
using SceneSelector;
using StatisticPlayer;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
    public struct PanelsLvl
    {
        public GameObject GndPanel;
        public GameObject ButtonPanel;
        public GameObject SettPanel;
        public GameObject RezultPanel;
    }
    public enum ActivPanel
    {
        GndPanel, ButtonPanel, SettPanel, RezultPanel
    }
    public class UIPanelsExecutor : IUIPanelsExecutor
    {
        public Action<ActivPanel> OnStateUI { get { return onStateUI; } set { onStateUI = value; } }
        private Action<ActivPanel> onStateUI;
        public Action<WinAudioSetting> OnParametrUI { get { return onParametrUI; } set { onParametrUI = value; } }
        private Action<WinAudioSetting> onParametrUI;
        public Action<Statistic> OnStatisticUI { get { return onStatisticUI; } set { onStatisticUI = value; } }
        private Action<Statistic> onStatisticUI;

        private WinAudioSetting winAudioSetting;
        private PanelsLvl panelsLvl;
        private ActivPanel activPanel;

        //Screen
        private List<string> textScreen;
        public List<string> TextScreen { get { return textScreen; } }
        private int indexCurrentScreen;
        public int IndexCurrentScreen { get { return indexCurrentScreen; } }
        private Resolution currentScreen;
        private Resolution[] resolutions, tempResolutions;
        //Statistic
        private Statistic currentStatistic;

        private IStatisticExecutor statistic;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IStatisticExecutor _statistic)
        {
            inputs = _inputs;
            statistic = _statistic;
        }

        public void Set(WinAudioSetting _winAudioSetting, PanelsLvl _panelsLvl)
        {
            OnEnable();
            winAudioSetting = new WinAudioSetting();
            winAudioSetting = _winAudioSetting;
            panelsLvl=new PanelsLvl();
            panelsLvl = _panelsLvl;
        }
        private void OnEnable()
        {
            inputs.OnEventUpdata += EventUpdata;
            statistic.OnUpdateStatistic += UpdateStatistic;
        }
        private void UpdateStatistic(Statistic _statistic)
        {
            currentStatistic= _statistic;
        }
        private void EventUpdata(InputData inputData)
        {
            if (inputData.Menu != 0){StateUI();}
        }
        private void StateUI()
        {
            onStateUI?.Invoke(activPanel);
        }
        public void CallGndPanel()
        {
            activPanel = ActivPanel.GndPanel;
            panelsLvl.GndPanel.SetActive(true);
            panelsLvl.ButtonPanel.SetActive(false);
            panelsLvl.SettPanel.SetActive(false);
            panelsLvl.RezultPanel.SetActive(false);
            GameTimer(true);
        }
        public void CallButtonPanel()
        {
            activPanel = ActivPanel.ButtonPanel;
            panelsLvl.GndPanel.SetActive(false);
            panelsLvl.ButtonPanel.SetActive(true);
            panelsLvl.SettPanel.SetActive(false);
            panelsLvl.RezultPanel.SetActive(false);
            GameTimer(false);
        }
        public void CallSettPanel()
        {
            activPanel = ActivPanel.SettPanel;
            panelsLvl.GndPanel.SetActive(false);
            panelsLvl.ButtonPanel.SetActive(false);
            panelsLvl.SettPanel.SetActive(true);
            panelsLvl.RezultPanel.SetActive(false);
            GameTimer(false);
        }
        public void CallRezultPanel()
        {
            activPanel = ActivPanel.RezultPanel;
            panelsLvl.GndPanel.SetActive(false);
            panelsLvl.ButtonPanel.SetActive(false);
            panelsLvl.SettPanel.SetActive(false);
            panelsLvl.RezultPanel.SetActive(true);
            GameTimer(false);
            GetCurrentStatistic();
        }
        private void GameTimer(bool isRun)
        {
            if (isRun){Time.timeScale = 1;}
            else {Time.timeScale = 0;}
        }

        #region EPROM
        private void SetResolution(Resolution currentScreen)
        {
            PlayerPrefs.SetInt("CurrentWidth", currentScreen.width);
            PlayerPrefs.SetInt("CurrentHeight", currentScreen.height);
        }
        private Resolution GetResolution()
        {
            Resolution temp = new Resolution();
            temp.width = PlayerPrefs.GetInt("CurrentWidth");
            temp.height = PlayerPrefs.GetInt("CurrentHeight");
            return temp;
        }
        private void SetAudioParametr(float muzVol, float efectVol)
        {
            PlayerPrefs.SetFloat("CurrentMuzVol", muzVol);
            PlayerPrefs.SetFloat("CurrentEfectVol", efectVol);
            winAudioSetting.MuzVol = muzVol;
            winAudioSetting.EfectVol = efectVol;
        }
        private void GetAudioParametr()
        {
            winAudioSetting.MuzVol = PlayerPrefs.GetFloat("CurrentMuzVol");
            winAudioSetting.EfectVol = PlayerPrefs.GetFloat("CurrentEfectVol");
        }
        #endregion

        #region Screen
        public void ScreenSet()
        {
            currentScreen = GetResolution();
            SetCurrentResolution(currentScreen);

            textScreen = new List<string>();
            tempResolutions = Screen.resolutions;

            for (int i = 0; i < tempResolutions.Length; i++)
            {
                if (tempResolutions[i].width >= winAudioSetting.MinWidth & tempResolutions[i].height >= winAudioSetting.MinHeight)
                {
                    resolutions = CreatResolution(tempResolutions[i], resolutions);
                    textScreen.Add($"{tempResolutions[i].width}x{tempResolutions[i].height}");
                }
            }

            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == currentScreen.width & resolutions[i].height == currentScreen.height)
                {
                    indexCurrentScreen = i; break;
                }
            }
        }
        private Resolution[] CreatResolution(Resolution intObject, Resolution[] massivObject)
        {
            if (massivObject != null)
            {
                int newLength = massivObject.Length + 1;
                Array.Resize(ref massivObject, newLength);
                massivObject[newLength - 1] = intObject;
                return massivObject;
            }
            else
            {
                massivObject = new Resolution[] { intObject };
                return massivObject;
            }
        }
        private void SetCurrentResolution(Resolution _currentScreen)
        {
            if (_currentScreen.width == 0 & _currentScreen.height == 0)
            {
                _currentScreen.width = winAudioSetting.MinWidth;
                _currentScreen.height = winAudioSetting.MinHeight;
            }

            bool fullScreen = true;
            Screen.SetResolution(_currentScreen.width, _currentScreen.height, fullScreen);
        }
        public void SetNewResolution(int indexDrop)
        {
            currentScreen.width = resolutions[indexDrop].width;
            currentScreen.height = resolutions[indexDrop].height;
            SetResolution(currentScreen);

            SetCurrentResolution(currentScreen);
        }
        #endregion

        #region Audio
        public void AudioSet()
        {
            GetAudioParametr();
            onParametrUI?.Invoke(winAudioSetting);
        }
        public void SetNewAudio(WinAudioSetting _winAudioSetting)
        {
            SetAudioParametr(_winAudioSetting.MuzVol, _winAudioSetting.EfectVol);
            AudioSet();
        }
        #endregion

        #region Statistic
        private void GetCurrentStatistic()
        {
            onStatisticUI?.Invoke(currentStatistic);
        }
        #endregion
    }
}

