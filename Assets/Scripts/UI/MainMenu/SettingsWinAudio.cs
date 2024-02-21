using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsWinAudio : MonoBehaviour
    {
        [Header("Уровень музыки"), Range(0, 1)]
        public float MuzVol = 0.5f;
        [Header("Уровень эффектов"), Range(0, 1)]
        public float EfectVol = 0.5f;
        [Header("Звуковой файл - кнопка")]
        public AudioClip AudioClipButton;
        [Header("Звуковой файл - фон")]
        public AudioClip AudioClipGnd;

        [Header("Мин ширина(width)")]
        public int MinWidth = 1280;
        [Header("Мин высота(height)")]
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

