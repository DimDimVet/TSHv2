using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsUILvl : MonoBehaviour
    {
        [Header("MenuSceneIndex")]
        [SerializeField] private int menuSceneIndex = 0;
        [Header("GameSceneIndex")]
        [SerializeField] private int gameSceneIndex = 1;
        [Header("VictorySceneIndex")]
        [SerializeField] private int victorySceneIndex = 2;
        [Header("OverSceneIndex")]
        [SerializeField] private int overSceneIndex = 3;

        [Header("Уровень музыки"), Range(0, 1)]
        [SerializeField] private float muzVol = 0.5f;
        [Header("Уровень эффектов"), Range(0, 1)]
        [SerializeField] private float efectVol = 0.5f;
        [Header("Звуковой файл - кнопка")]
        [SerializeField] private AudioClip audioClipButton;
        [Header("Звуковой файл - фон")]
        [SerializeField] private AudioClip audioClipGnd;

        [Header("Мин ширина(width)")]
        [SerializeField] private int minWidth = 1280;
        [Header("Мин высота(height)")]
        [SerializeField] private int minHeight = 1024;

        [Header("Панели")]
        [SerializeField] private GameObject gndPanel;
        [SerializeField] private GameObject buttonPanel;
        [SerializeField] private GameObject settPanel;
        [SerializeField] private GameObject rezultPanel;

        private WinAudioSetting winAudioSetting;
        private AudioSource audioSource, audioSourceMuz;
        private bool isStopClass = false, isRun = false;

        private IUIPanelsExecutor panels;
        [Inject]
        public void Init(IUIPanelsExecutor _panels)
        {
            panels = _panels;
        }
        private void OnEnable()
        {
            panels.OnParametrUI += ParametrUI;
            panels.OnAudioClick += AudioClick;
        }
        private void AudioClick(bool isClick)
        {
            if (audioSource != null && isClick) { audioSource.Play(); }
        }
        private void ParametrUI(WinAudioSetting _winAudioSetting)
        {
            winAudioSetting = _winAudioSetting;
            if (audioSource != null)
            {
                audioSource.volume = winAudioSetting.EfectVol;
            }
            if (audioSourceMuz != null)
            {
                audioSourceMuz.volume = winAudioSetting.MuzVol;
            }
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                if (panels != null)
                {
                    WinAudioSetting winAudioSetting = new WinAudioSetting()
                    {
                        MuzVol = muzVol,
                        EfectVol = efectVol,
                        AudioClipButton = audioClipButton,
                        AudioClipGnd = audioClipGnd,
                        MinWidth = minWidth,
                        MinHeight = minHeight
                    };

                    PanelsLvl panelsLvl = new PanelsLvl()
                    {
                        GndPanel = gndPanel,
                        ButtonPanel = buttonPanel,
                        SettPanel = settPanel,
                        RezultPanel = rezultPanel
                    };

                    SceneIndex sceneIndex = new SceneIndex()
                    {
                        MenuSceneIndex= menuSceneIndex,
                        GameSceneIndex= gameSceneIndex,
                        VictorySceneIndex= victorySceneIndex,
                        OverSceneIndex= overSceneIndex
                    };
                    panels.Set(winAudioSetting, panelsLvl, sceneIndex);
                    panels.CallGndPanel();
                    isRun = true;
                    RunAudio();
                }
                else { isRun = false; }
            }
        }
        private void RunAudio()
        {
            panels.AudioSet();

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = winAudioSetting.AudioClipButton;
            audioSource.volume = winAudioSetting.EfectVol;

            audioSourceMuz = gameObject.AddComponent<AudioSource>();
            audioSourceMuz.clip = winAudioSetting.AudioClipGnd;
            audioSourceMuz.volume = winAudioSetting.MuzVol;
            audioSourceMuz.Play();
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }
    }
}

