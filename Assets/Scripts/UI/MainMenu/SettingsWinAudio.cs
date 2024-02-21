using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsWinAudio : MonoBehaviour
    {
        [Header("������� ������"), Range(0, 1)]
        public float MuzVol = 0.5f;
        [Header("������� ��������"), Range(0, 1)]
        public float EfectVol = 0.5f;
        [Header("�������� ���� - ������")]
        public AudioClip AudioClipButton;
        [Header("�������� ���� - ���")]
        public AudioClip AudioClipGnd;

        [Header("��� ������(width)")]
        public int MinWidth = 1280;
        [Header("��� ������(height)")]
        public int MinHeight = 1024;

        private bool isStopClass = false, isRun = false;

        private IUIPanelsExecutor panels;
        [Inject]
        public void Init(IUIPanelsExecutor _panels)
        {
            panels = _panels;
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
                        MuzVol = MuzVol,
                        EfectVol = EfectVol,
                        AudioClipButton = AudioClipButton,
                        AudioClipGnd = AudioClipGnd,
                        MinWidth = MinWidth,
                        MinHeight = MinHeight
                    };

                    isRun = true;
                    panels.SetWinAudio(winAudioSetting);
                }
                else { isRun = false; }
            }
        }

        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

    }
}

