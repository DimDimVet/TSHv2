using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicGndPanel : MonoBehaviour
    {
        [Header("Кнопка Меню")]
        [SerializeField] private Button menuButton;

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
            if (_activPanel == ActivPanel.GndPanel) { ButtonPanel(); }
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
            menuButton.onClick.AddListener(ButtonPanel);
        }
        private void ButtonPanel()
        {
            panels.CallButtonPanel();
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

    }
}

