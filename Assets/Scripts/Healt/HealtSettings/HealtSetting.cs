using UnityEngine;

namespace Healt
{
    [CreateAssetMenu(fileName = "HealtSettings", menuName = "ScriptableObjects/HealtSettings")]
    public class HealtSetting : ScriptableObject
    {
        [Header("Уровень здоровья")]
        public int HealtCount = 1000;
        [Header("Стоимость объекта")]
        public int CostObject = 1;
    }
}

