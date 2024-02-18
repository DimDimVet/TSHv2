using System;

namespace Healt
{
    public interface IHealt
    {
        public Action<int, int> OnGetDamage { get; set; }
        public void SetDamage(int getHash, int damage);
        public void StatisticHealt(int getHash, int currentHealt, int maxHealt);
        public Action<int, int, int> OnStatisticHealt { get; set; }
        public void DeadObject(int getHash, int costObject);
        public Action<int, bool> OnIsDead { get; set; }
        public Action<int> OnStatisticScore { get; set; }
    }
}

