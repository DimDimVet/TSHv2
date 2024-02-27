using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(fileName = "LootSettings", menuName = "ScriptableObjects/LootSettings")]
    public class LootSettings : ScriptableObject
    {
        [Header("������� +Healt")]
        public int PlusHealt = 1;
        [Header("�������� ������ Loot")]
        public float SpeedMoveLoot = 1f;
        
    }
}