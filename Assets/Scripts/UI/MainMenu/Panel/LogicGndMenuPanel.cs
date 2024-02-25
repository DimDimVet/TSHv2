using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LogicGndMenuPanel : MonoBehaviour
    {
        [Header("Текст наименования")]
        [SerializeField] private string mainName;
        [Header("Объект текст ")]
        [SerializeField] private Text uiText;
        [Header("Скорость вывода текста"), Range(0, 5)]
        [SerializeField] private float timer;
        [Header("Цикличность")]
        [SerializeField] private bool loop = false;
        private int index;
        private float countTime = 0;
        private bool isStop = false, isRunWrite = false;

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
                    isRun = true;
                }
                else { isRun = false; }
            }
        }
        private void AddWrite(bool _isRun)
        {
            if (!isStop & _isRun & mainName != "" & uiText != null)
            {
                index++;
                if (index <= mainName.Length)
                {
                    uiText.text = mainName.Substring(0, index);
                    panels.AudioClick();
                }
                else
                {
                    if (loop) { index = 0; }
                    else { isStop = true; ButtonPanel(); return; }
                }
            }
        }
        private void ButtonPanel()
        {
            panels.AudioMuz();
            panels.CallButtonPanel(true);
        }
        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            UpTimer();
        }
        private void UpTimer()
        {
            if (isStop) { return; }
            if (countTime <= timer)
            {
                countTime += Time.deltaTime;
            }
            else
            {
                if (isRunWrite) { isRunWrite = !isRunWrite; }
                else { isRunWrite = !isRunWrite; AddWrite(isRunWrite); }
                countTime = 0;
            }
        }
    }
}

