using System;
using UnityEngine;

namespace Pools
{
    public class Pool
    {
        private Element[] containerObject;
        private Transform containerTransform;
        private bool isFabric = false;

        public Pool(GameObject rezultFabric, Transform containerTransform, bool _isFabric)
        {
            isFabric = _isFabric;
            this.containerTransform = containerTransform;
            containerObject = new Element[] { CreatObjectZFabric(rezultFabric) };

        }
        private Element CreatObjectZFabric(GameObject rezultFabric, bool isSetActiv = false)
        {
            GameObject temp = rezultFabric;
            temp.SetActive(isSetActiv);
            Element element = new Element
            {
                Object = temp,
                HashCodeObject = temp.GetHashCode(),
            };
            SetTransform(element);
            return element;
        }
        private void SetTransform(Element element)
        {
            if (element.Object.transform != containerTransform)
            {
                element.Object.transform.position = containerTransform.position;
                element.Object.transform.rotation = containerTransform.rotation;
            }
        }
        private void SetTransformPoint(Element element, Transform _containerTransform)
        {
            if (element.Object.transform.position != _containerTransform.position)
            {
                element.Object.transform.position = _containerTransform.position;
                element.Object.transform.rotation = _containerTransform.rotation;
            }
        }
        public GameObject GetObjectFabric(Transform _containerTransform)
        {
            int index = ControlQueueFabric(out bool isQueue);
            if (isQueue)
            {
                SetTransformPoint(containerObject[index], _containerTransform);
                containerObject[index].Object.gameObject.SetActive(true);
                return containerObject[index].Object;
            }
            else
            {
                return null;
            }

        }
        private int ControlQueueFabric(out bool isQueue)
        {
            for (int i = 0; i < containerObject.Length; i++)
            {
                if (!containerObject[i].Object.activeSelf)
                {
                    isQueue = true;
                    return i;
                }
            }
            isQueue = false;
            return -1;

        }
        public void NewObjectQueue(GameObject newGameObject)
        {
            int newLength = containerObject.Length + 1;
            Array.Resize(ref containerObject, newLength);

            containerObject[newLength - 1] = CreatObjectZFabric(newGameObject);
        }
        public bool ReternObject(int _hash)
        {
            int hash = _hash;

            for (int i = 0; i < containerObject.Length; i++)
            {
                if (containerObject[i].HashCodeObject == hash)
                {
                    containerObject[i].Object.SetActive(false);
                    containerObject[i].Object.transform.position = containerTransform.position;
                    containerObject[i].Object.transform.rotation = containerTransform.rotation;
                    return true;
                }
            }
            return false;
        }
    }
}
