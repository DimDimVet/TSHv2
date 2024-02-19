using System;
using UnityEngine;

namespace Healt
{
    public interface IHealt
    {
        public Action<int, int> OnGetDamage { get; set; }
        public void SetDamage(int getHash, int damage, Vector3 directionDamage);
        public void StatisticHealt(int getHash, int currentHealt, int maxHealt);
        public Action<int, int, int> OnStatisticHealt { get; set; }
        public void DeadObject(int getHash, int costObject);
        public Action<int, bool, Vector3> OnIsDead { get; set; }
        public Action<int, int> OnStatisticScore { get; set; }
    }
}

