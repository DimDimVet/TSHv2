using UnityEngine;

public class PatternClass : MonoBehaviour
{
    private bool isStopClass = false, isRun = false;
    private void Awake()
    {

    }
    private void OnEnable()
    {

    }
    void Start()
    {
        SetClass();
    }

    private void SetClass()
    {
        if (!isRun)
        {
            if (true) { isRun = true; }
            else { isRun = false; }
        }
    }

    void Update()
    {
        if (isStopClass) { return; }
        if (!isRun) { SetClass(); }
        RunUpdate();
    }
    private void RunUpdate()
    {

    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }
    private void OnDisable()
    {

    }
}
