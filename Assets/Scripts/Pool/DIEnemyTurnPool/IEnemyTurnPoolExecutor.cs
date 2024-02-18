using UnityEngine;

namespace Pools
{
    public interface IEnemyTurnPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}