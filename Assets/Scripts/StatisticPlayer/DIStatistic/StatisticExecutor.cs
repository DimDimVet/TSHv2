using Bulls;
using Healts;
using Registrator;
using System;
using UnityEngine;
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
            if (tempCount == null) { return false; }

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
        private void GetDamage(int getHash, int damage, TypeBullet typeBullet)
        {
            if (getHash == thisHash) { statistic.RezultOutDamag += damage; }
            if (getHash != thisHash)
            {
                statistic.RezultInDamag += damage;
                statistic.CurrentInDamag = damage;
                SetStatistic(statistic);
            }
        }
        private void UpdateStatistic(Statistic statistic)
        {
            onUpdateStatistic?.Invoke(statistic);
        }
        public void SetStatistic(Statistic statistic)
        {
            PlayerPrefs.SetInt("ThisHash", statistic.ThisHash);
            PlayerPrefs.SetInt("CountEnemy", statistic.CountEnemy);
            PlayerPrefs.SetInt("KillEnemy", statistic.KillEnemy);
            PlayerPrefs.SetInt("RezultCost", statistic.RezultCost);
            PlayerPrefs.SetInt("RezultOutDamag", statistic.RezultOutDamag);
            PlayerPrefs.SetInt("RezultInDamag", statistic.RezultInDamag);
            PlayerPrefs.SetInt("CurrentInDamag", statistic.CurrentInDamag);
            UpdateStatistic(statistic);
        }
        public Statistic GetStatistic()
        {
            Statistic statistic = new Statistic();
            statistic.ThisHash= PlayerPrefs.GetInt("ThisHash");
            statistic.CountEnemy = PlayerPrefs.GetInt("CountEnemy");
            statistic.KillEnemy = PlayerPrefs.GetInt("KillEnemy");
            statistic.RezultCost = PlayerPrefs.GetInt("RezultCost");
            statistic.RezultOutDamag = PlayerPrefs.GetInt("RezultOutDamag");
            statistic.RezultInDamag = PlayerPrefs.GetInt("RezultInDamag");
            statistic.CurrentInDamag = PlayerPrefs.GetInt("CurrentInDamag");
            return statistic;
        }
    }
}

