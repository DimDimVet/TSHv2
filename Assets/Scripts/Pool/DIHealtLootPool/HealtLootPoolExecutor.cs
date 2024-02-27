using Bulls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pools
{
    public class HealtLootPoolExecutor : IHealtLootPoolExecutor
    {
        private Pool pool;
        [Inject]
        private PlayerTurnSleeve.Factory bullFactory;
        private void AddPull(Transform containerTransform)
        {
            Bull rezult = bullFactory.Create();
            pool = new Pool(rezult.gameObject, containerTransform, true);
        }

        public GameObject GetObject(float direction, Transform containerTransform)
        {
            if (pool == null) { AddPull(containerTransform); }
            GameObject tempGameObject = pool.GetObjectFabric(containerTransform);

            if (tempGameObject != null) { return tempGameObject; }
            else
            {
                Bull rezult = bullFactory.Create();
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