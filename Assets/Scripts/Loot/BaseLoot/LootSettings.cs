using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(fileName = "LootSettings", menuName = "ScriptableObjects/LootSettings")]
    public class LootSettings : ScriptableObject
    {
        [Header("��������")]
        public int Healt = 1;
    }
}

