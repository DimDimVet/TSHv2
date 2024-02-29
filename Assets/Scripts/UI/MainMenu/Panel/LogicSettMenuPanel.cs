using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicSettMenuPanel : MonoBehaviour
    {
        [Header("Dropdown разрешений")]
        [SerializeField] private Dropdown screenDropdown;
        [Header("Slider звука")]
        [SerializeField] private Slider muzSlider;
        [SerializeField] private Slider effectSlider;
        [Header(" нопка продолжить")]
        [SerializeField] private Button returnButton;
        private WinAudioSetting winAudioSetting;

        private bool isStopClass = false, isRun = false;

        private IUIPanelsExecutor panels;
        [Inject]
        public void Init(IUIPanelsExecutor _panels)
        {
            panels = _panels;
        }

        private void OnEnable()
        {
            panels.OnStateUI += EscClick;
            panels.OnParametrUI += ParametrUI;
            panels.OnSetResolution += SetResolution;
        }
        private void SetResolution(Resolution resolution)
        {
            Screen.SetResolution(resolution.width, resolution.height, true);
        }
        private void EscClick(ActivPanel _activPanel)
        {
            if (_activPanel == ActivPanel.SettPanel) { ButtonPanel(); }
        }
        private void ParametrUI(WinAudioSetting _winAudioSetting)
        {
            winAudioSetting = _winAudioSetting;
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
                    panels.ScreenSet(Screen.resolutions);
                    screenDropdown.ClearOptions();
                    screenDropdown.AddOptions(panels.TextScreen);
                    screenDropdown.value = panels.IndexCurrentScreen;

                    panels.AudioSet();
                    muzSlider.value = winAudioSetting.MuzVol;
                    effectSlider.value = winAudioSetting.EfectVol;

                    isRun = true;
                    SetEventButton();
                }
                else { isRun = false; }
            }
        }
        private void SetEventButton()
        {
            returnButton.onClick.AddListener(ButtonPanel);
            screenDropdown.onValueChanged.AddListener(NewResolution);
        }
        private void ButtonPanel()
        {
            panels.CallButtonPanel(true);
        }
        private void NewResolution(int indexDrop)
        {
            panels.SetNewResolution(indexDrop);
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            AudioControl();
        }
        private void AudioControl()
        {
            if (muzSlider.value != winAudioSetting.MuzVol || effectSlider.value != winAudioSetting.EfectVol)
            {
                winAudioSetting.MuzVol = muzSlider.value;
                winAudioSetting.EfectVol = effectSlider.value;
                panels.SetNewAudio(winAudioSetting);
            }
        }
    }

}