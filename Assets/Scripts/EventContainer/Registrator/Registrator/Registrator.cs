using UnityEngine;
using Zenject;

namespace Registrator
{
    public class Registrator : MonoBehaviour
    {
        [SerializeField] private TypeObject typeObject=TypeObject.Other;
        private int thisHash, tempChildrenHash;
        private int[] childrenHash;

        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }

        void Start()
        {
            GetObject();
        }
        private void GetObject()
        {
            thisHash = gameObject.GetHashCode();

            Construction registrator = new Construction
            {
                Hash = thisHash,
                IsDead = false,
                ChildrenHash = GetChildren(),
                TypeObject = typeObject,
                CameraComponent = GetComponent<Camera>(),
                Transform = gameObject.transform,
            };
            dataList.SetData(registrator);
        }
        private int[] GetChildren()
        {
            Collider[] childrens = GetComponentsInChildren<Collider>();
            childrenHash = new int[childrens.Length];

            if (childrens != null)
            {
                for (int i = 0; i < childrens.Length; i++)
                {
                    tempChildrenHash = childrens[i].gameObject.GetHashCode();
                    if (tempChildrenHash != thisHash)
                    {
                        childrenHash[i] = tempChildrenHash;
                    }
                }
            }
            return childrenHash;
        }
    }
}

