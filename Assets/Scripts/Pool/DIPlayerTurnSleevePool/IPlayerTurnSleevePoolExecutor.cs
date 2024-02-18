using UnityEngine;

namespace Pools
{
    public interface IPlayerTurnSleevePoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}