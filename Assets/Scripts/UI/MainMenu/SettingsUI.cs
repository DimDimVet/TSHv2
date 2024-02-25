using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("MenuSceneIndex")]
        [SerializeField] protected int menuSceneIndex = 0;
        [Header("GameSceneIndex")]
        [SerializeField] protected int gameSceneIndex = 1;
        [Header("VictorySceneIndex")]
        [SerializeField] protected int victorySceneIndex = 2;
        [Header("OverSceneIndex")]
        [SerializeField] protected int overSceneIndex = 3;

        [Header("Уровень музыки"), Range(0, 1)]
        [SerializeField] protected float muzVol = 0.5f;
        [Header("Уровень эффектов"), Range(0, 1)]
        [SerializeField] protected float efectVol = 0.5f;
        [Header("Звуковой файл - кнопка")]
        [SerializeField] protected AudioClip audioClipButton;
        [Header("Звуковой файл - фон")]
        [SerializeField] protected AudioClip audioClipGnd;

        [Header("Мин ширина(width)")]
        [SerializeField] protected int minWidth = 1280;
        [Header("Мин высота(height)")]
        [SerializeField] protected int minHeight = 1024;

        [Header("Панели")]
        [SerializeField] protected GameObject gndPanel;
        [SerializeField] protected GameObject buttonPanel;
        [SerializeField] protected GameObject settPanel;
        [SerializeField] protected GameObject rezultPanel;

        protected WinAudioSetting winAudioSetting;
        protected AudioSource audioSource, audioSourceMuz;
        protected bool isStopClass = false, isRun = false;

        protected IUIPanelsExecutor panels;
        [Inject]
        public void Init(IUIPanelsExecutor _panels)
        {
            panels = _panels;
        }
        private void OnEnable()
        {
            panels.OnParametrUI += ParametrUI;
            panels.OnAudioClick += AudioClick;
            panels.OnAudioMuz += AudioMuz;
        }
        private void AudioClick(bool isClick)
        {
            if (audioSource != null && isClick) { audioSource.Play(); }
        }
        private void AudioMuz(bool isClick)
        {
            if (audioSourceMuz != null && isClick) { audioSourceMuz.Play(); }
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
        protected virtual void SetClass()
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
        protected virtual void RunAudio()
        {
            panels.AudioSet();

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = winAudioSetting.AudioClipButton;
            audioSource.volume = winAudioSetting.EfectVol;

            audioSourceMuz = gameObject.AddComponent<AudioSource>();
            audioSourceMuz.clip = winAudioSetting.AudioClipGnd;
            audioSourceMuz.volume = winAudioSetting.MuzVol;
            AudioMuz(true);
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }
    }
}

