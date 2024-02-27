using Healts;
using Registrator;
using StatisticPlayer;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SelectorScene : MonoBehaviour
    {
        //public bool isStart = false;
        private int thisHash;
        private int countEnemy;
        private bool isStopClass = false, isRun = false;

        private IStatisticExecutor statisticExecutor;
        private IListDataExecutor dataList;
        private IUIPanelsExecutor panels;
        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor, IUIPanelsExecutor _panels,
                         IListDataExecutor _dataList, IStatisticExecutor _statisticExecutor)
        {
            healtExecutor = _healtExecutor;
            panels = _panels;
            dataList = _dataList;
            statisticExecutor = _statisticExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnIsDead += IsDead;
            statisticExecutor.OnUpdateStatistic += UpdateStatistic;
        }
        private void IsDead(int getHash, bool isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash && isRun) { panels.CallOverScene(); /*isStart = true;*/}
        }
        private void UpdateStatistic(Statistic statistic)
        {
            countEnemy= statistic.CountEnemy;
            if (countEnemy == 0 && isRun) { panels.CallVictoryScene(); }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                thisHash = dataList.GetPlayer().Hash;
                if (thisHash!=0)
                {
                    isRun = true;
                }
                else { isRun = false; }
            }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

        //private void FixedUpdate()
        //{
        //    RunUpdate();
        //}
        //private void RunUpdate()
        //{
        //    if (isStart)
        //    {

        //        isStart = !isStart;
        //    }
        //}
    }
}