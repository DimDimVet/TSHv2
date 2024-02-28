using Bulls;
using UnityEngine;
using Zenject;

namespace Healts
{
    public class Healt : MonoBehaviour
    {
        [SerializeField] private HealtSetting settingsHealt;
        private int healtCount, maxHealt, costObject;
        private int thisHash;
        private TypeBullet[] typeBullets;
        private bool isStopRun = false;

        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor)
        {
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnGetDamage += ControlDamage;
            healtExecutor.OnHealing += Healing;
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
            typeBullets = settingsHealt.TypeBullets;
            healtExecutor.StatisticHealt(thisHash, healtCount, maxHealt);
        }

        void Update()
        {
            if (isStopRun) { return; }
        }
        private void ControlDamage(int getHash, int damage, TypeBullet typeBullet)
        {
            for (int i = 0; i < typeBullets.Length; i++)
            {
                if (thisHash == getHash && !isStopRun && typeBullets[i] == typeBullet)
                {
                    if (healtCount > 0) { healtCount = healtCount - damage; healtExecutor.StatisticHealt(getHash, healtCount, maxHealt); }
                    if (healtCount <= 0) { healtExecutor.DeadObject(getHash, costObject); isStopRun = true; }
                }
            }
            

        }
        private void Healing(int getHash, int healing)
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

