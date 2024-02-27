using Bulls;
using UnityEngine;

namespace Healts
{
    [CreateAssetMenu(fileName = "HealtSettings", menuName = "ScriptableObjects/HealtSettings")]
    public class HealtSetting : ScriptableObject
    {
        [Header("Уровень здоровья")]
        public int HealtCount = 1000;
        [Header("Стоимость объекта")]
        public int CostObject = 1;
        [Header("Получать урон по типу пули:")]
        public TypeBullet[] TypeBullets;
    }
}

