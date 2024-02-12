using Registrator;
using System;
using UnityEngine;
using Zenject;

namespace EnemyLogic
{
    public class ScanEnemyExecutor : IScanEnemyExecutor
    {
        private Construction player;
        private int tempHash;
        public Action<Construction, int> OnFindPlayer { get { return onFindPlayer; } set { onFindPlayer = value; } }
        private Action<Construction, int> onFindPlayer;
        public Action<int> OnLossPlayer { get { return onLossPlayer; } set { onLossPlayer = value; } }
        private Action<int> onLossPlayer;

        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }

        public void FindPlayer(Collider[] hitColl, int senderHash)
        {
            if (player.Hash == 0) { player = dataList.GetPlayer(); }
            else
            {
                for (int i = 0; i < hitColl.Length; i++)
                {
                    tempHash = hitColl[i].gameObject.GetHashCode();
                    if (player.Hash == tempHash) { RezultFindPlayer(player, senderHash); return; }
                }
                LossPlayer(senderHash);
            }
        }
        private void RezultFindPlayer(Construction player, int recipientHash)
        {
            onFindPlayer?.Invoke(player, recipientHash);
        }
        private void LossPlayer(int recipientHash)
        {
            onLossPlayer?.Invoke(recipientHash);
        }
    }
}

