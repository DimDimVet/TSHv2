using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "MoveSettings", menuName = "ScriptableObjects/MoveSettings")]
    public class MoveSettings : ScriptableObject
    {
        [Header("�������� ������"), Range(0, 50)]
        public float SpeedForward = 5f;
        [Header("�������� �����"), Range(0, 50)]
        public float SpeedBack = 5f;
        [Header("�������� ��������"), Range(0, 50)]
        public float SpeedTurn = 5f;
    }
}

