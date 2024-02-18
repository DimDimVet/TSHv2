using UnityEngine;

namespace Pools
{
    public interface IPlayerTurnPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}