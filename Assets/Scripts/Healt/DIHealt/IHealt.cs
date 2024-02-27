using Bulls;
using System;
using UnityEngine;

namespace Healts
{
    public interface IHealt
    {
        public Action<int, int, TypeBullet> OnGetDamage { get; set; }
        public void SetDamage(int getHash, int damage, Vector3 _directionDamage, TypeBullet typeBullet);
        public void StatisticHealt(int getHash, int currentHealt, int maxHealt);
        public Action<int, int, int> OnStatisticHealt { get; set; }
        public void DeadObject(int getHash, int costObject);
        public Action<int, bool, Vector3> OnIsDead { get; set; }
        public Action<int, int> OnStatisticScore { get; set; }
        public void Healing(int getHash, int healt);
        public Action<int, int> OnHealing { get; set; }
    }
}

