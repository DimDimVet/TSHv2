using Input;
using UI;
using UnityEngine;
using Zenject;

namespace AudioScene
{
    public class AudioShoot : MonoBehaviour
    {
        [SerializeField] private AudioClipSetting audioClipSetting;
        private Mode mode;
        protected AudioSource audioSource;
        protected int thisHash;
        private WinAudioSetting winAudioSetting;
        private bool isStopClass = false, isRun = false;

        private IUIPanelsExecutor panels;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IUIPanelsExecutor _panels)
        {
            audioShoot = _audioShoot;
            panels=_panels;
        }
        private void OnEnable()
        {
            audioShoot.OnShootAudio += ShootAudio;
            panels.OnParametrUI += ParametrUI;
        }
        private void ParametrUI(WinAudioSetting _winAudioSetting)
        {
            winAudioSetting = _winAudioSetting;
            if (audioSource != null)
            {
                audioSource.volume = winAudioSetting.EfectVol;
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
                audioSource = gameObject.AddComponent<AudioSource>();
                thisHash = gameObject.GetHashCode();
                if (audioSource != null)
                {
                    mode = audioClipSetting.Mode;
                    audioSource.clip = audioClipSetting.AudioClip;
                    audioSource.loop = false;
                    audioSource.Stop();

                    isRun = true;
                }
                else { isRun = false; }
            }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }
        protected virtual void ShootAudio(int _thisHash, Mode mode)
        {
            if (thisHash == _thisHash)
            {
                audioSource.Play();
            }
        }
    }
}

