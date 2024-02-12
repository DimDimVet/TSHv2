using UnityEngine;

namespace EnemyLogic
{
    [CreateAssetMenu(fileName = "MoveEnemySettings", menuName = "ScriptableObjects/MoveEnemySettings")]
    public class MoveEnemySettings : ScriptableObject
    {
        [Header("Скорость движения")]
        public float SpeedMove = 5f;
        [Header("Ускорение")]
        public float Acceleration = 7f;
        [Header("Скорость поворота")]
        public float SpeedAngle = 120f;
        [Header("Стоп дистанция до цели")]
        public float StopDistance = 15f;
    }
}

