using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(fileName = "LootSettings", menuName = "ScriptableObjects/LootSettings")]
    public class LootSettings : ScriptableObject
    {
        [Header("Уровень +Healt")]
        public int PlusHealt = 1;
        [Header("Скорость вылета Loot")]
        public float SpeedMoveLoot = 1f;
        
    }
}