using UnityEngine;

namespace Pools
{
    public interface IParticleShootEnemyRifPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}