using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LogicGndPanel : MonoBehaviour
    {
        [Header("Кнопка Меню")]
        [SerializeField] private Button menuButton;
        [SerializeField] private GameObject gndPanel;
        private bool isStopClass = false, isRun = false;
        private void Awake()
        {

        }
        private void OnEnable()
        {

        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                if (true) { isRun = true; }
                else { isRun = false; }
            }
        }

        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {

        }
        private void FixedUpdate()
        {

        }
        private void LateUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

