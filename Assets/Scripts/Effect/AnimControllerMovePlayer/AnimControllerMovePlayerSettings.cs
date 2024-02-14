using UnityEngine;

namespace Effect
{
    [CreateAssetMenu(fileName = "AnimatorPlayerSettings", menuName = "ScriptableObjects/AnimatorPlayerSettings")]
    public class AnimControllerMovePlayerSettings : ScriptableObject
    {
        [Header("Скорость проигрывания")]
        public float SpeedAnim = 1f;
        [Header("Гусеница назад")]
        public string TankPlayerTrackBack = "";
        [Header("Гусеница вперед")]
        public string TankPlayerTrackForward = "";
        [Header("Гусеница влево")]
        public string TankPlayerTrackLeft = "";
        [Header("Гусеница вправо")]
        public string TankPlayerTrackRight = "";

    }
}

