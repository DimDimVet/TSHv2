using Healt;
using Registrator;
using System;
using Zenject;

namespace StatisticPlayer
{
    public struct Statistic
    {
        public int ThisHash;
        public int CountEnemy;
        public int RezultCost;
        public int RezultOutDamag;
        public int RezultInDamag;
    }
    public class StatisticExecutor : IStatisticExecutor
    {
        public Action<Statistic> OnUpdateStatistic { get { return onUpdateStatistic; } set { onUpdateStatistic = value; } }
        private Action<Statistic> onUpdateStatistic;
        private int thisHash;
        private Statistic statistic;

        private IHealt healtExecutor;
        private IListDataExecutor data;
        [Inject]
        public void Init(IListDataExecutor _data, IHealt _healtExecutor)
        {
            data = _data;
            healtExecutor = _healtExecutor;
        }
        public void InitStatistic()
        {
            thisHash = data.GetPlayer().Hash;
            statistic = new Statistic
            {
                ThisHash = thisHash,
                CountEnemy = data.GetEnemys().Length,
                RezultCost = 0,
                RezultOutDamag = 0,
                RezultInDamag = 0,
            };
            OnEnable();
        }
        private void OnEnable()
        {
            healtExecutor.OnStatisticScore += StatisticScore;
            healtExecutor.OnGetDamage += GetDamage;
        }
        private void StatisticScore(int cost)
        {
            statistic.RezultCost += cost;
            UpdateStatistic(statistic);
        }
        private void GetDamage(int getHash, int damage)
        {
            if (getHash == thisHash) { statistic.RezultOutDamag += damage; }
            if (getHash != thisHash) { statistic.RezultInDamag += damage; }
            UpdateStatistic(statistic);
        }
        private void UpdateStatistic(Statistic statistic)
        {
            onUpdateStatistic?.Invoke(statistic);
        }
    }
}

