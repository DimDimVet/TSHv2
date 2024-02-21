using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicButtonPanel : MonoBehaviour
    {
        [Header("Кнопка Настройка")]
        [SerializeField] private Button settButton;
        [Header("Кнопка Меню")]
        [SerializeField] private Button mainMenuButton;
        [Header("Кнопка переиграть")]
        [SerializeField] private Button reBootButton;
        [Header("Кнопка Результат")]
        [SerializeField] private Button rezultButton;
        [Header("Кнопка продолжить")]
        [SerializeField] private Button returnButton;

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
        }
        private void EscClick(ActivPanel _activPanel)
        {
            if (_activPanel == ActivPanel.ButtonPanel) { GndPanel(); }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                if (panels != null) { isRun = true; SetEventButton(); }
                else { isRun = false; }
            }
        }
        private void SetEventButton()
        {
            settButton.onClick.AddListener(SettPanel);
            mainMenuButton.onClick.AddListener(MainMenu);
            reBootButton.onClick.AddListener(ReBootScene);
            rezultButton.onClick.AddListener(RezultPanel);
            returnButton.onClick.AddListener(GndPanel);
        }
        private void SettPanel()
        {
            panels.CallSettPanel();
        }
        private void MainMenu()
        {
            //Time.timeScale = 1f;
            //SceneManager.LoadScene(sceneSetting.MenuSceneIndex);
        }
        private void ReBootScene()
        {
            //Time.timeScale = 1f;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void RezultPanel()
        {
            panels.CallRezultPanel();
        }
        private void GndPanel()
        {
            panels.CallGndPanel();
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

    }

}


