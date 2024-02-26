using Healt;
using Input;
using Registrator;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CursorControler : MonoBehaviour
    {
        [SerializeField] private Texture2D defaultCur;
        [SerializeField] private Texture2D activCur;
        [SerializeField, Range(0, 512)] private float positionDefaultCurX, positionDefaultCurY;
        private Vector2 setDefaultCur;

        private IUIPanelsExecutor panels;
        [Inject]
        public void Init(IUIPanelsExecutor _panels)
        {
            panels = _panels;
        }
        private void Start()
        {
            setDefaultCur = new Vector2(positionDefaultCurX, positionDefaultCurY);
            Cursor.SetCursor(defaultCur, setDefaultCur, CursorMode.Auto);
        }
        private void OnEnable()
        {
            panels.OnSelectCursor += SwitchCursor;
        }

        private void SwitchCursor(bool isActivCursor)
        {
            if (isActivCursor) { Cursor.SetCursor(activCur, setDefaultCur, CursorMode.Auto); }
            else { Cursor.SetCursor(defaultCur, setDefaultCur, CursorMode.Auto); }
        }
    }
}

