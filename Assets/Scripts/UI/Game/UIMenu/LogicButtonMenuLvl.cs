using AudioScene;
using Input;
using SceneSelector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicButtonMenuLvl : MonoBehaviour
    {
        [SerializeField] private AudioSetting audioSetting;
        [SerializeField] private SceneSetting sceneSetting;

        [Header("Кнопка Меню")]
        [SerializeField] private Button menuButton;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject sliderHealt;
        [Header("Кнопка Настройка")]
        [SerializeField] private Button settButton;
        [SerializeField] private GameObject settPanel;
        [Header("Кнопка Меню")]
        [SerializeField] private Button mainMenuButton;
        [Header("Кнопка переиграть")]
        [SerializeField] private Button reBootButton;
        [Header("Кнопка продолжить")]
        [SerializeField] private Button continButton;
        private AudioSource audioSource, audioSourceMuz;
        private bool isTriggerEsc = false;
        private bool isStopClass = false, isRun = false;

        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
        }
        private void OnEnable()
        {
            inputs.OnEventUpdata += EventUpdata;
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                if (menuButton != null)
                {
                    SetEventButton();
                    SetAudio();
                    menuPanel.SetActive(false);
                    settPanel.SetActive(false);

                    isRun = true;
                }
                else { isRun = false; }
            }
        }
        private void SetEventButton()
        {
            menuButton.onClick.AddListener(MenuGame);
            settButton.onClick.AddListener(SettGame);
            mainMenuButton.onClick.AddListener(MainMenuGame);
            reBootButton.onClick.AddListener(ReBootGame);
            continButton.onClick.AddListener(ContinGame);
        }
        private void SetAudio()
        {
            if (audioSetting != null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = audioSetting.AudioClipButton;
                audioSource.volume = (audioSetting.EfectVol);

                audioSourceMuz = gameObject.AddComponent<AudioSource>();
                audioSourceMuz.clip = audioSetting.AudioClipGnd;
                audioSourceMuz.volume = (audioSetting.MuzVol);
                audioSourceMuz.Play();
            }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

        private void EventUpdata(InputData inputData)
        {
            if (inputData.Menu != 0)
            {
                if (!isTriggerEsc){MenuGame();}
                else{ ContinGame();}
            }
        }
        private void AudioClick()
        {
            audioSource.Play();
        }
        private void MenuGame()
        {
            if (!isTriggerEsc) { isTriggerEsc = !isTriggerEsc; }
            AudioClick();
            Time.timeScale = 0f;
            menuPanel.SetActive(true);
            sliderHealt.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
        }
        private void SettGame()
        {
            AudioClick();
            settPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        private void MainMenuGame()
        {
            AudioClick();
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneSetting.MenuSceneIndex);
        }
        private void ReBootGame()
        {
            ContinGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void ContinGame()
        {
            if (isTriggerEsc) { isTriggerEsc = !isTriggerEsc; }
            AudioClick();
            Time.timeScale = 1f;
            menuPanel.SetActive(false);
            sliderHealt.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
        }
    }
}

