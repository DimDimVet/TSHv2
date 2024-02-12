using UnityEngine;

namespace EnemyLogic
{
    [CreateAssetMenu(fileName = "ScanEnemySettings", menuName = "ScriptableObjects/ScanEnemySettings")]
    public class ScanEnemySettings : ScriptableObject
    {
        [Header("Диаметр коллайдера")]
        public float DiametrCollider = 40f;
    }
}

