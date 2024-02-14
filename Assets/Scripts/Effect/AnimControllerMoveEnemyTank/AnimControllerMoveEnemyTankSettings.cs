using UnityEngine;

namespace Effect
{
    [CreateAssetMenu(fileName = "AnimatorEnemyTankSettings", menuName = "ScriptableObjects/AnimatorEnemyTankSettings")]
    public class AnimControllerMoveEnemyTankSettings : ScriptableObject
    {
        [Header("�������� ������������")]
        public float SpeedAnim = 1f;
        [Header("�������� �����")]
        public string TankEnemyTrackBack = "";
        [Header("�������� ������")]
        public string TankEnemyTrackForward = "";
        [Header("�������� �����")]
        public string TankEnemyTrackLeft = "";
        [Header("�������� ������")]
        public string TankEnemyTrackRight = "";
    }
}

