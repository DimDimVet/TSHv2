using UnityEngine;

namespace Pools
{
    public interface IParticleShootEnemyTurnPoolExecutor
    {
        GameObject GetObject(float direction, Transform containerTransform);
        void ReternObject(int _hash);
    }
}