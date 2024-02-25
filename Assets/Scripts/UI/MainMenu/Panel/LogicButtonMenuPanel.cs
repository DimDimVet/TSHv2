using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicButtonMenuPanel : MonoBehaviour
    {
        [Header("Кнопка Игра")]
        [SerializeField] private Button gameButton;
        [Header("Кнопка Настройка")]
        [SerializeField] private Button settButton;
        [Header("Кнопка Результат")]
        [SerializeField] private Button rezultButton;
        [Header("Кнопка Выход")]
        [SerializeField] private Button exitButton;

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
            if (_activPanel == ActivPanel.ButtonPanel) { ExitPanel(); }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                if (panels != null) { isRun = true; SetEventButton();}
                else { isRun = false; }
            }
        }
        private void SetEventButton()
        {
            settButton.onClick.AddListener(SettPanel);
            gameButton.onClick.AddListener(GameMenu);
            rezultButton.onClick.AddListener(RezultPanel);
            exitButton.onClick.AddListener(ExitPanel);
        }
        private void SettPanel()
        {
            panels.CallSettPanel(true);
        }
        private void GameMenu()
        {
            panels.CallGameMenu();
        }
        private void RezultPanel()
        {
            panels.CallRezultPanel(true);
        }
        private void ExitPanel()
        {
            panels.ExitGame();
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }
    }
}


