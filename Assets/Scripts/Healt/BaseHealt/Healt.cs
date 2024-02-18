using UnityEngine;
using Zenject;

namespace Healt
{
    public class Healt : MonoBehaviour
    {
        [SerializeField] private HealtSetting settingsHealt;
        private int healtCount, maxHealt, costObject;
        private int thisHash;
        private bool isRun = false, isStopRun = false;

        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor)
        {
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnGetDamage += ControlDamage;
        }
        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();
            healtCount = settingsHealt.HealtCount;
            maxHealt = healtCount;
            costObject = settingsHealt.CostObject;
            healtExecutor.StatisticHealt(thisHash, healtCount, maxHealt);
        }

        void Update()
        {
            if (isStopRun) { return; }
        }
        private void ControlDamage(int getHash, int damage)
        {
            if (thisHash == getHash && !isStopRun)
            {
                if (healtCount > 0) { healtCount = healtCount - damage; healtExecutor.StatisticHealt(getHash, healtCount, maxHealt); }
                if (healtCount <= 0) { healtExecutor.DeadObject(getHash, costObject); isStopRun = true; }
                Debug.Log($"{gameObject.name} -> {healtCount}");
            }
        }
        public void Healing(int getHash, int healing)
        {
            if (thisHash == getHash & healtCount > 0)
            {
                healtCount = healtCount + healing;
                if (healtCount > maxHealt)
                {
                    healtCount = maxHealt;
                    healtExecutor.StatisticHealt(getHash, healtCount, maxHealt);
                }
            }

        }
    }
}

