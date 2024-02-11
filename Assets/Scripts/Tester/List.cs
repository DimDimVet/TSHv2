using Registrator;
using UnityEngine;
using Zenject;

public class List : MonoBehaviour
{
    public bool isTest = false;

    private IListDataExecutor dataList;
    [Inject]
    public void Init(IListDataExecutor _dataList)
    {
        dataList = _dataList;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTest)
        {
            var gd = dataList.GetData();
            for (int i = 0; i < gd.Length; i++)
            {
                Debug.Log($"{gd[i].TypeObject} {gd[i].Hash} ");

            }
            isTest=false;
        }
    }
}
