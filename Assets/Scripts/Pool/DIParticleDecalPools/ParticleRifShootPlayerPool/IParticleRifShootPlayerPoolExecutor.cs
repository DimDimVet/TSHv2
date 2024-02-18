using UnityEngine;

namespace Pools
{
    public interface IParticleRifShootPlayerPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}