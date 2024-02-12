using UnityEngine;

namespace EnemyLogic
{
    [CreateAssetMenu(fileName = "MoveEnemySettings", menuName = "ScriptableObjects/MoveEnemySettings")]
    public class MoveEnemySettings : ScriptableObject
    {
        [Header("�������� ��������")]
        public float SpeedMove = 5f;
        [Header("���������")]
        public float Acceleration = 7f;
        [Header("�������� ��������")]
        public float SpeedAngle = 120f;
        [Header("���� ��������� �� ����")]
        public float StopDistance = 15f;
    }
}

