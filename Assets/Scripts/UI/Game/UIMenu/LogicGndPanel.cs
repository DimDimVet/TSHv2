using Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public struct ElementGrid
    {
        public GameObject PrefabType;
        public GameObject Element;
    }
    public class LogicGndPanel : MonoBehaviour
    {
        [Header("������ ����")]
        [SerializeField] private Button menuButton;
        //setGrid
        [Header("��������� ����� - ����� �harging")]
        [SerializeField] private int spacingX�harging = 0;
        [SerializeField] private int spacingY�harging = 0;
        [Header("��������� ����� - ����� Gun")]
        [SerializeField] private int spacingXGun = 0;
        [SerializeField] private int spacingYGun = 0;
        [Header("��������� ����� - ����� Rif")]
        [SerializeField] private int spacingXRif = 0;
        [SerializeField] private int spacingYRif = 0;
        //end setGrid
        [Header("��������� �����")]
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private GameObject element�harging;
        [SerializeField] private GameObject elementGun;
        [SerializeField] private GameObject elementRif;
        private GameObject tempElement;
        private ElementGrid element;
        private List<ElementGrid> elements;
        private Vector2 mode�harging, modeGun, modeRif;
        private Mode currentMode;

        private bool isStopClass = false, isRun = false;

        private IInputPlayerExecutor inputs;
        private IUIPanelsExecutor panels;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IUIPanelsExecutor _panels)
        {
            inputs = _inputs;
            panels = _panels;
        }
        private void OnEnable()
        {
            inputs.OnEventUpdata += CurrentMode;
            panels.OnStateUI += EscClick;
            panels.OnChargingUpdate += UpdateElementGrid;
        }

        private void CurrentMode(InputData data)
        {
            currentMode = data.ModeAction;
            if (elements != null)
            {
                panels.CurrentMode(currentMode); 
            }
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
                if (panels != null)
                {
                    SetGrid();
                    SetEventButton();
                    isRun = true;
                }
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
        private void SetGrid()
        {
            mode�harging.x = spacingX�harging;
            mode�harging.y = spacingY�harging;
            modeGun.x = spacingXGun;
            modeGun.y = spacingYGun;
            modeRif.x = spacingXRif;
            modeRif.y = spacingYRif;

            SetGridElement();
        }
        private void CreatList(GameObject prefabType, GameObject elementGrid)
        {
            elementGrid.SetActive(false);
            element = new ElementGrid { PrefabType = prefabType, Element = elementGrid };

            if (elements == null) { elements = new List<ElementGrid>() { element }; return; }
            else { elements.Add(element); }
        }
        private void SetGridElement()
        {
            tempElement = GameObject.Instantiate(element�harging, grid.transform.position,
                                                            grid.transform.rotation, grid.transform);
            CreatList(element�harging, tempElement);

            Charging[] tempParametr= panels.ChargingGetParametr();
            for (int i = 0; i < tempParametr.Length; i++){ SetModeElement(tempParametr[i]); }

            panels.CurrentMode(currentMode);
        }
        private void SetModeElement(Charging charging)
        {
            if (charging.Mode== Mode.Turn && charging.CurrentCountClip!=0)
            {
                for (int i = 0; i < charging.CurrentCountClip; i++)
                {
                    tempElement = GameObject.Instantiate(elementGun, grid.transform.position,
                                                          grid.transform.rotation, grid.transform);
                    CreatList(elementGun, tempElement);
                }
            }

            if (charging.Mode == Mode.AvtoRif && charging.CurrentCountClip != 0)
            {
                for (int i = 0; i < charging.CurrentCountClip; i++)
                {
                    tempElement = GameObject.Instantiate(elementRif, grid.transform.position,
                                                          grid.transform.rotation, grid.transform);
                    CreatList(elementRif, tempElement);
                }
            }
        }
        private void UpdateElementGrid(Charging charging)
        {
            int tempCount = 0;
            if (charging.IsClipReLoad)
            {
                grid.spacing = mode�harging;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].PrefabType == element�harging)
                    {
                        elements[i].Element.SetActive(true);
                    }
                    else
                    {
                        elements[i].Element.SetActive(false);
                    }
                }
            }
            if (Mode.Turn == charging.Mode)
            {
                grid.spacing = modeGun;
                if (!charging.IsClipReLoad)
                {
                    tempCount = 0;
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (elements[i].PrefabType == elementGun & charging.CurrentCountClip > tempCount)
                        {
                            elements[i].Element.SetActive(true);
                        }
                        else
                        {
                            elements[i].Element.SetActive(false);
                        }
                    }
                }
            }
            if (Mode.AvtoRif == charging.Mode)
            {
                grid.spacing = modeRif;
                if (!charging.IsClipReLoad)
                {
                    tempCount = 0;
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (elements[i].PrefabType == elementRif & charging.CurrentCountClip > tempCount)
                        {
                            elements[i].Element.SetActive(true);
                            tempCount++;
                        }
                        else
                        {
                            elements[i].Element.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}

