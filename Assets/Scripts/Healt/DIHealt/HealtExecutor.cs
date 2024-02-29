using Bulls;
using Registrator;
using System;
using UnityEngine;
using Zenject;

namespace Healts
{
    public class HealtExecutor : IHealt
    {
        private Construction[] dataList;

        public Action<int, int> OnStatisticScore { get { return onStatisticScore; } set { onStatisticScore = value; } }
        private Action<int, int> onStatisticScore;
        public Action<int, int, TypeBullet> OnGetDamage { get { return onGetDamage; } set { onGetDamage = value; } }
        private Action<int, int, TypeBullet> onGetDamage;
        public Action<int, int> OnHealing { get { return onHealing; } set { onHealing = value; } }
        private Action<int, int> onHealing;
        public Action<int, int, int> OnStatisticHealt { get { return onStatisticHealt; } set { onStatisticHealt = value; } }
        private Action<int, int, int> onStatisticHealt;
        public Action<int, bool, Vector3> OnIsDead { get { return onIsDead; } set { onIsDead = value; } }
        private Action<int, bool, Vector3> onIsDead;
        public Action<int, bool> OnIsDeadAndDirection { get { return onIsDeadAndDirection; } set { onIsDeadAndDirection = value; } }
        private Action<int, bool> onIsDeadAndDirection;
        
        private Vector3 directionDamage;

        private IListDataExecutor data;
        [Inject]
        public void Init(IListDataExecutor _data)
        {
            data = _data;
        }
        private void GetDamage(int getHash, int damage, TypeBullet typeBullet)
        {
            onGetDamage?.Invoke(getHash, damage, typeBullet);
        }
        private void SetHealt(int getHash, int damage)
        {
            onHealing?.Invoke(getHash, damage);
        }
        public void SetDamage(int getHash, int damage, Vector3 _directionDamage,TypeBullet typeBullet)
        {
            directionDamage= _directionDamage;
            if (dataList == null) { dataList = data.GetData(); }
            for (int i = 0; i < dataList.Length; i++)
            {
                if (dataList[i].Hash == getHash && !dataList[i].IsDead)
                { GetDamage(getHash, damage, typeBullet); }

                for (int j = 0; j < dataList[i].ChildrenHash.Length; j++)
                {
                    if (dataList[i].ChildrenHash[j] == getHash && !dataList[i].IsDead)
                    { GetDamage(dataList[i].Hash, damage, typeBullet); }
                }
            }
        }
        public void Healing(int getHash, int healt)
        {
            if (dataList == null) { dataList = data.GetData(); }
            for (int i = 0; i < dataList.Length; i++)
            {
                if (dataList[i].Hash == getHash && !dataList[i].IsDead)
                { SetHealt(getHash, healt); }

                for (int j = 0; j < dataList[i].ChildrenHash.Length; j++)
                {
                    if (dataList[i].ChildrenHash[j] == getHash && !dataList[i].IsDead)
                    { SetHealt(dataList[i].Hash, healt); }
                }
            }
        }
        public void StatisticHealt(int getHash, int currentHealt, int maxHealt)
        {
            onStatisticHealt?.Invoke(getHash, currentHealt, maxHealt);
        }
        private void StatisticScore(int getHash, int cost)
        {
            onStatisticScore?.Invoke(getHash, cost);
        }
        public void DeadObject(int getHash, int costObject)
        {
            for (int i = 0; i < dataList.Length; i++)
            {
                if (dataList[i].Hash == getHash)
                {
                    dataList[i].IsDead = true;
                    onIsDead?.Invoke(getHash, true, directionDamage);
                    StatisticScore(getHash,costObject);
                }
            }
        }
    }
}

