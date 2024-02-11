using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "TurnSettings", menuName = "ScriptableObjects/TurnSettings")]
    public class TurnSettings : ScriptableObject
    {
        [Header("�������� ��������"), Range(0, 50)]
        public float SpeedTurn = 5f;

        [Header("�������� �� �(���. �� ����� 0.007)")]
        public float MaxOffSetX = 0.007f;
    }
}

