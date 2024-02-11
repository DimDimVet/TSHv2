using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "MoveSettings", menuName = "ScriptableObjects/MoveSettings")]
    public class MoveSettings : ScriptableObject
    {
        [Header("Скорость вперед"), Range(0, 50)]
        public float SpeedForward = 5f;
        [Header("Скорость назад"), Range(0, 50)]
        public float SpeedBack = 5f;
        [Header("Скорость поворота"), Range(0, 50)]
        public float SpeedTurn = 5f;
    }
}

