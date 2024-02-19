using StatisticPlayer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private Text countPlayerText;
    [SerializeField] private Text countEnemysText;
    [SerializeField] private Text infoCountPlayerText;

    [SerializeField, Range(0, 2)] private float timerAlfa;
    private float countAlfa = 0.5f;
    private Color currColorAlfa;
    private bool isUpDate = false;

    private bool isStopClass = false, isRun = false;

    private IStatisticExecutor statisticExecutor;
    [Inject]
    public void Init(IStatisticExecutor _statisticExecutor)
    {
        statisticExecutor = _statisticExecutor;
    }
    private void OnEnable()
    {
        statisticExecutor.OnUpdateStatistic += UpdateStatistic;
    }
    private void UpdateStatistic(Statistic statistic)
    {
        isUpDate = !isUpDate;
        currColorAlfa.a = 1f;

        countPlayerText.text = $"{statistic.RezultCost}";
        countEnemysText.text = $"{statistic.CountEnemy}";
        infoCountPlayerText.text = $"{statistic.CurrentInDamag}";
    }
    void Start()
    {
        SetClass();
    }
    private void SetClass()
    {
        if (!isRun)
        {
            if (statisticExecutor.InitStatistic())
            {
                currColorAlfa = infoCountPlayerText.color;
                currColorAlfa.a = 0f;
                infoCountPlayerText.color = currColorAlfa;

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

    private void LateUpdate()
    {
        InfoCountAlfa();
    }
    private void InfoCountAlfa()
    {
        if (isUpDate)
        {
            if (countAlfa <= timerAlfa)
            {
                countAlfa = countAlfa + 0.1f;
            }
            else
            {
                countAlfa = 0;
                currColorAlfa.a = currColorAlfa.a - 0.1f;
                if (currColorAlfa.a <= 0) { isUpDate = !isUpDate; }
                infoCountPlayerText.color = currColorAlfa;
            }
        }
    }
}
