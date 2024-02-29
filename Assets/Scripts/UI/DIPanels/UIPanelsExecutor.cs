using Input;
using StatisticPlayer;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public struct SceneIndex
    {
        public int MenuSceneIndex;
        public int GameSceneIndex;
        public int VictorySceneIndex;
        public int OverSceneIndex;
    }
    public struct Charging
    {
        public Mode Mode;
        public int CurrentCountClip;
        public bool IsClipReLoad;
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
        public Action<bool> OnAudioClick { get { return onAudioClick; } set { onAudioClick = value; } }
        private Action<bool> onAudioClick;
        public Action<bool> OnAudioMuz { get { return onAudioMuz; } set { onAudioMuz = value; } }
        private Action<bool> onAudioMuz;
        public Action<Statistic> OnStatisticUI { get { return onStatisticUI; } set { onStatisticUI = value; } }
        private Action<Statistic> onStatisticUI;
        public Action<Mode> OnCurrentMode { get { return onCurrentMode; } set { onCurrentMode = value; } }
        private Action<Mode> onCurrentMode;
        public Action<Charging> OnChargingUpdate { get { return onChargingUpdate; } set { onChargingUpdate = value; } }
        private Action<Charging> onChargingUpdate;
        public Action<Resolution> OnSetResolution { get { return onSetResolution; } set { onSetResolution = value; } }
        private Action<Resolution> onSetResolution;
        public Action<bool> OnSelectCursor { get { return onSelectCursor; } set { onSelectCursor = value; } }
        private Action<bool> onSelectCursor;

        private WinAudioSetting winAudioSetting;
        private PanelsLvl panelsLvl;
        private SceneIndex sceneIndex;
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
        //Charging
        private Charging charging;
        private Charging[] chargings;
        private Mode currentMode;

        private IStatisticExecutor statistic;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IStatisticExecutor _statistic)
        {
            inputs = _inputs;
            statistic = _statistic;
        }
        public void Set(WinAudioSetting _winAudioSetting, PanelsLvl _panelsLvl, SceneIndex _sceneIndex)
        {
            OnEnable();
            winAudioSetting = new WinAudioSetting();
            winAudioSetting = _winAudioSetting;
            panelsLvl = new PanelsLvl();
            panelsLvl = _panelsLvl;
            sceneIndex = new SceneIndex();
            sceneIndex = _sceneIndex;
        }
        private void OnEnable()
        {
            inputs.OnEventUpdata += EventUpdata;
            statistic.OnUpdateStatistic += UpdateStatistic;
        }
        private void UpdateStatistic(Statistic _statistic)
        {
            currentStatistic = _statistic;
        }
        private void EventUpdata(InputData inputData)
        {
            if (inputData.Menu != 0) { StateUI(); }
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
            AudioClick();
        }
        public void CallButtonPanel(bool isGnd=false)
        {
            activPanel = ActivPanel.ButtonPanel;
            if (isGnd) { panelsLvl.GndPanel.SetActive(isGnd); }
            else { panelsLvl.GndPanel.SetActive(isGnd); }
            panelsLvl.ButtonPanel.SetActive(true);
            panelsLvl.SettPanel.SetActive(false);
            panelsLvl.RezultPanel.SetActive(false);
            GameTimer(false);
            AudioClick();
        }
        public void CallSettPanel(bool isGnd = false)
        {
            activPanel = ActivPanel.SettPanel;
            if (isGnd) { panelsLvl.GndPanel.SetActive(isGnd); }
            else { panelsLvl.GndPanel.SetActive(isGnd); }
            panelsLvl.ButtonPanel.SetActive(false);
            panelsLvl.SettPanel.SetActive(true);
            panelsLvl.RezultPanel.SetActive(false);
            GameTimer(false);
            AudioClick();
        }
        public void CallRezultPanel(bool isGnd = false,bool isRezultPanel=false)
        {
            activPanel = ActivPanel.RezultPanel;
            if (isGnd) { panelsLvl.GndPanel.SetActive(isGnd); }
            else { panelsLvl.GndPanel.SetActive(isGnd); }
            if (isRezultPanel) { }
            else 
            {
                panelsLvl.ButtonPanel.SetActive(isRezultPanel);
                panelsLvl.SettPanel.SetActive(isRezultPanel);
            }
            panelsLvl.RezultPanel.SetActive(true);
            GameTimer(false);
            GetCurrentStatistic();
            AudioClick();
        }
        private void GameTimer(bool isRun)
        {
            if (isRun) { Time.timeScale = 1; }
            else { Time.timeScale = 0; }
        }

        #region Cursor
        public void SelectCursor(bool isSelect)
        {
            onSelectCursor?.Invoke(isSelect);
        }
        #endregion

        #region Scene
        public void ReBootScene()
        {
            GameTimer(true);
            AudioClick();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void MainMenu()
        {
            GameTimer(true);
            AudioClick();
            SceneManager.LoadScene(sceneIndex.MenuSceneIndex);
        }
        public void CallGameMenu()
        {
            GameTimer(true);
            AudioClick();
            SceneManager.LoadScene(sceneIndex.GameSceneIndex);
        }
        public void CallVictoryScene()
        {
            GameTimer(false);
            AudioClick();
            SceneManager.LoadScene(sceneIndex.VictorySceneIndex);
        }
        public void CallOverScene()
        {
            GameTimer(false);
            AudioClick();
            SceneManager.LoadScene(sceneIndex.OverSceneIndex);
        }
        public void ExitGame()
        {
            AudioClick();
            Application.Quit();
        }
        #endregion

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
        public void ScreenSet(Resolution[] _resolutions)
        {
            currentScreen = GetResolution();
            SetCurrentResolution(currentScreen);

            textScreen = new List<string>();
            tempResolutions = _resolutions;

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
            onSetResolution?.Invoke(_currentScreen);
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
        public void AudioClick()
        {
            onAudioClick?.Invoke(true);
        }
        public void AudioMuz()
        {
            onAudioMuz?.Invoke(true);
        }
        #endregion

        #region Statistic
        private void GetCurrentStatistic()
        {
            if (currentStatistic.ThisHash == 0) { currentStatistic = statistic.GetStatistic(); }
            onStatisticUI?.Invoke(currentStatistic);
        }
        #endregion

        #region ChargingParametr
        public void ChargingSetParametr(Mode mode, int currentCountClip)
        {
            if (chargings == null)
            {
                charging = new Charging() { Mode = mode, CurrentCountClip = currentCountClip };
                chargings = new Charging[] { charging };
            }
            else
            {
                charging = new Charging() { Mode = mode, CurrentCountClip = currentCountClip };
                int newLength = chargings.Length + 1;
                Array.Resize(ref chargings, newLength);
                chargings[newLength - 1] = charging;
            }

        }
        public Charging[] ChargingGetParametr()
        {
            return chargings;
        }
        public void CurrentMode(Mode _currentMode)
        {
            currentMode = _currentMode;
            onCurrentMode?.Invoke(currentMode);
        }
        public void ChargingUpdate(Mode mode, bool isClipReLoad, int currentCountClip)
        {
            if (currentMode == mode)
            {
                charging.Mode = mode;
                charging.CurrentCountClip = currentCountClip;
                charging.IsClipReLoad = isClipReLoad;
                onChargingUpdate?.Invoke(charging);
            }
        }
        #endregion
    }
}

