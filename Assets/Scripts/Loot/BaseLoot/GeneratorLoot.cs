using Healts;
using Pools;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class GeneratorLoot : MonoBehaviour
    {
        public Transform ContainerHealtLoot;
        private int thisHash;
        private bool isRun = false, isStopRun = false;

        private IHealtLootPoolExecutor lootPool;
        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor, IHealtLootPoolExecutor _lootPool)
        {
            healtExecutor = _healtExecutor;
            lootPool = _lootPool;
        }
        private void OnEnable()
        {
            isStopRun = false;
            healtExecutor.OnIsDead += IsDead;
        }
        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();
        }
        private void GetRun()
        {
            if (!isRun)
            {
                isRun = true;
            }
        }
        void Update()
        {
            if (isStopRun) { return; }
            if (!isRun) { GetRun(); }
        }
        private void IsDead(int getHash, bool isDead, Vector3 direction)
        {
            if (thisHash == getHash)
            {
                lootPool.GetObject(1, ContainerHealtLoot);
                isStopRun = isDead;
            }
        }

    }
}

