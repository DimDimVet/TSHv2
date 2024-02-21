using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsUILvl : MonoBehaviour
    {
        [Header("������� ������"), Range(0, 1)]
        [SerializeField] private float muzVol = 0.5f;
        [Header("������� ��������"), Range(0, 1)]
        [SerializeField] private float efectVol = 0.5f;
        [Header("�������� ���� - ������")]
        [SerializeField] private AudioClip audioClipButton;
        [Header("�������� ���� - ���")]
        [SerializeField] private AudioClip audioClipGnd;

        [Header("��� ������(width)")]
        [SerializeField] private int minWidth = 1280;
        [Header("��� ������(height)")]
        [SerializeField] private int minHeight = 1024;

        [Header("������")]
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
                    panels.Set(winAudioSetting, panelsLvl);
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

