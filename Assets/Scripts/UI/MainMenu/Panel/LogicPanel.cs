using AudioScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LogicPanel : MonoBehaviour
    {
        public AudioSetting AudioSetting { get { return audioSetting; } set { audioSetting = value; } }
        [SerializeField] private AudioSetting audioSetting;

        [Header("Кнопка Назад")]
        [SerializeField] private Button returnButton;
        [SerializeField] private GameObject thisPanel;
        public GameObject ThisPanel { get { return thisPanel; } set { thisPanel = value; } }
        private AudioSource audioSource;
        private void Start()
        {
            if (returnButton != null)
            {
                SetEventReturnButton();
                SetEventButton();
                SetPanel();
            }
            else { print($"Не заполнены поля в {gameObject.name}"); return; }

            if (audioSetting != null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = audioSetting.AudioClipButton;
                audioSource.volume = (audioSetting.EfectVol);
            }
        }
        //private void OnEnable()
        //{
        //    OnUpDateAudioParametr += UpDateAudio;
        //}
        //private void OnDisable()
        //{
        //    OnUpDateAudioParametr -= UpDateAudio;
        //}
        private void UpDateAudio()
        {
            audioSource.volume = (audioSetting.EfectVol);
        }
        private void SetEventReturnButton()
        {
            returnButton.onClick.AddListener(ReturnPanel);
        }
        public virtual void SetEventButton()
        {
        }
        public virtual void SetPanel()
        {
        }
        public void AudioClick()
        {
            audioSource.Play();
        }
        public virtual void ReturnPanel()
        {
            AudioClick();
            if (thisPanel != null) { thisPanel.SetActive(false); }
            //IsRunMainPanel(true);
        }
    }
}

