using UnityEngine;

namespace Pools
{
    public interface IParticleTurnShootPlayerPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}