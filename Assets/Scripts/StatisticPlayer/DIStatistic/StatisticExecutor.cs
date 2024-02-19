using Healt;
using Registrator;
using System;
using System.Diagnostics;
using Zenject;

namespace StatisticPlayer
{
    public struct Statistic
    {
        public int ThisHash;
        public int CountEnemy;
        public int KillEnemy;
        public int RezultCost;
        public int RezultOutDamag;
        public int RezultInDamag;
        public int CurrentInDamag;
    }
    public class StatisticExecutor : IStatisticExecutor
    {
        public Action<Statistic> OnUpdateStatistic { get { return onUpdateStatistic; } set { onUpdateStatistic = value; } }
        private Action<Statistic> onUpdateStatistic;

        private int thisHash;
        Construction[] tempCount;
        private Statistic statistic;

        private IHealt healtExecutor;
        private IListDataExecutor data;
        [Inject]
        public void Init(IListDataExecutor _data, IHealt _healtExecutor)
        {
            data = _data;
            healtExecutor = _healtExecutor;
        }
        public bool InitStatistic()
        {
            tempCount = data.GetEnemys();
            if (tempCount==null) { return false; }

            thisHash = data.GetPlayer().Hash;
            statistic = new Statistic
            {
                ThisHash = thisHash,
                CountEnemy = tempCount.Length,
                RezultCost = 0,
                RezultOutDamag = 0,
                RezultInDamag = 0,
            };
            UpdateStatistic(statistic);
            OnEnable();
            return true;
        }
        private void OnEnable()
        {
            healtExecutor.OnStatisticScore += StatisticScore;
            healtExecutor.OnGetDamage += GetDamage;
        }
        private void StatisticScore(int getHash, int cost)
        {
            if (getHash != thisHash)
            {
                statistic.RezultCost += cost;
                statistic.CountEnemy--;
                statistic.KillEnemy++;
            }
        }
        private void GetDamage(int getHash, int damage)
        {
            if (getHash == thisHash) { statistic.RezultOutDamag += damage; }
            if (getHash != thisHash)
            { 
                statistic.RezultInDamag += damage; 
                statistic.CurrentInDamag = damage;
                UpdateStatistic(statistic);
            }
            
        }
        private void UpdateStatistic(Statistic statistic)
        {
            onUpdateStatistic?.Invoke(statistic);
        }
    }
}

