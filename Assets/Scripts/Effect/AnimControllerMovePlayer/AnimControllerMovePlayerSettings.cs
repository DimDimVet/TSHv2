using UnityEngine;

namespace Effect
{
    [CreateAssetMenu(fileName = "AnimatorPlayerSettings", menuName = "ScriptableObjects/AnimatorPlayerSettings")]
    public class AnimControllerMovePlayerSettings : ScriptableObject
    {
        [Header("�������� ������������")]
        public float SpeedAnim = 1f;
        [Header("�������� �����")]
        public string TankPlayerTrackBack = "";
        [Header("�������� ������")]
        public string TankPlayerTrackForward = "";
        [Header("�������� �����")]
        public string TankPlayerTrackLeft = "";
        [Header("�������� ������")]
        public string TankPlayerTrackRight = "";

    }
}

