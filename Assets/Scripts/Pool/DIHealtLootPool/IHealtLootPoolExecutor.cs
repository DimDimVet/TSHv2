using UnityEngine;

namespace Pools
{
    public interface IHealtLootPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}