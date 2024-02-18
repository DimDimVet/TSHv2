using UnityEngine;

namespace Pools
{
    public interface IPlayerRifSleevePoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}