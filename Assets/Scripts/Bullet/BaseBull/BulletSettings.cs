using UnityEngine;

namespace Bulls
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "ScriptableObjects/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [Header("���� ����-�������")]
        public TypeBullet TypeBullet;
        [Header("���� �����")]
        public float SpeedBullet = 5f;
        [Header("����� ����� ����")]
        public float KillTime = 5f;
        [Header("������� ����������"), Range(0, 1)]
        public float DiametrColl = 0.1f;
        [Header("�����")]
        public int Damage = 1;
        [Header("������� ������������ ������"), Range(0, 100)]
        public float PercentDamage = 50f;

    }
}

