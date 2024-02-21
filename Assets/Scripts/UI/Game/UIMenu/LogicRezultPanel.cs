using StatisticPlayer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicRezultPanel : MonoBehaviour
    {
        [Header("Поле KillEnemy")]
        [SerializeField] private Text killEnemy;
        [Header("Поле RezultCost")]
        [SerializeField] private Text rezultCost;
        [Header("Поле RezultOutDamag")]
        [SerializeField] private Text rezultOutDamag;
        [Header("Поле RezultInDamag")]
        [SerializeField] private Text rezultInDamag;
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
            panels.OnStatisticUI += StatisticUI;
        }

        private void StatisticUI(Statistic _statistic)
        {
            killEnemy.text = $"{_statistic.KillEnemy}";
            rezultCost.text = $"{_statistic.RezultCost}";
            rezultOutDamag.text = $"{_statistic.RezultOutDamag}";
            rezultInDamag.text = $"{_statistic.RezultInDamag}";
        }

        private void EscClick(ActivPanel _activPanel)
        {
            if (_activPanel == ActivPanel.RezultPanel) { ButtonPanel(); }
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
                    isRun = true;
                    SetEventButton();
                }
                else { isRun = false; }
            }
        }
        private void SetEventButton()
        {
            returnButton.onClick.AddListener(ButtonPanel);
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


