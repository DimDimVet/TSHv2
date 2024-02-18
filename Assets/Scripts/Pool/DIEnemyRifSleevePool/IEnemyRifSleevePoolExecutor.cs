using UnityEngine;

namespace Pools
{
    public interface IEnemyRifSleevePoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}