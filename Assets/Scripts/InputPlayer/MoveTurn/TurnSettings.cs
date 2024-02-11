using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "TurnSettings", menuName = "ScriptableObjects/TurnSettings")]
    public class TurnSettings : ScriptableObject
    {
        [Header("Скорость вращения"), Range(0, 50)]
        public float SpeedTurn = 5f;

        [Header("Смещение по Х(рек. не более 0.007)")]
        public float MaxOffSetX = 0.007f;
    }
}

