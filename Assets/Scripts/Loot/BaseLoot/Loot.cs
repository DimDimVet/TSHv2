using Registrator;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private LootSettings lootSettings;
        private int tempHash, thisHash;
        protected int healt;
        private Construction[] data;
        private Rigidbody rbThisObject;
        [SerializeField, Range(1, 100)] private float forceImpulse = 50f;
        private bool isRun = false, isStopRun = false;

        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }
        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();
            healt = lootSettings.Healt;
        }
        private void GetRun()
        {
            if (!isRun)
            {
                rbThisObject = GetComponent<Rigidbody>();
                if (!(rbThisObject is Rigidbody)) { gameObject.AddComponent<Rigidbody>(); }
                rbThisObject.AddForce(Vector3.up * forceImpulse, ForceMode.Impulse);

                data = dataList.GetData();
                if (data != null) { isRun = true; return; }
                isRun = false;
            }
        }
        void Update()
        {
            if (isStopRun) { return; }
            if (!isRun) { GetRun(); }
        }

        private void OnTriggerEnter(Collider other)
        {
            CollisionObject(other.gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            CollisionObject(other.gameObject);
        }
        private bool CollisionObject(GameObject gameObject)
        {
            if (gameObject != null)
            {
                tempHash = gameObject.GetHashCode();
                if (tempHash == thisHash) { return false; }
                FindPlayer(tempHash);
            }
            return false;
        }
        private void FindPlayer(int hash)
        {
            if (hash == 0 || data == null) { return; }

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Hash == hash & data[i].TypeObject == TypeObject.Player)
                { Executor(data[i]); ReternLoot(); }
            }

        }
        protected virtual void Executor(Construction player)
        {
        }
        protected virtual void ReternLoot()
        {
        }
    }
}

