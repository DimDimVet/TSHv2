using UnityEngine;

namespace Pools
{
    public interface IPlayerRifPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}