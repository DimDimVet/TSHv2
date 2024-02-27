using Loot;
using UnityEngine;
using Zenject;

namespace Pools
{
    public class HealtLootPoolExecutor : IHealtLootPoolExecutor
    {
        private Pool pool;
        [Inject]
        private HealtLoot.Factory healtLootFactory;
        private void AddPull(Transform containerTransform)
        {
            HealtLoot rezult = healtLootFactory.Create();
            pool = new Pool(rezult.gameObject, containerTransform, true);
        }

        public GameObject GetObject(float direction, Transform containerTransform)
        {
            if (pool == null) { AddPull(containerTransform); }
            GameObject tempGameObject = pool.GetObjectFabric(containerTransform);

            if (tempGameObject != null) { return tempGameObject; }
            else
            {
                HealtLoot rezult = healtLootFactory.Create();
                pool.NewObjectQueue(rezult.gameObject);
                return pool.GetObjectFabric(containerTransform);
            }
        }
        public void ReternObject(int _hash)
        {
            pool.ReternObject(_hash);
        }
    }
}