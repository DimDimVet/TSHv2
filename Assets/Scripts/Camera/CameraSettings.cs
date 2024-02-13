using Input;
using UnityEngine;

namespace CameraMain
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [Header("Установка по оси X")]
        public float AxesX = 0f;
        [Header("Установка по оси Y")]
        public float AxesY = 0f;
        [Header("Установка по оси Z")]
        public float AxesZ = -30f;

        public Vector3 GetAxes()
        {
            return new Vector3(AxesX, AxesY, AxesZ);
        }

        [Header("Скорость перемещения камеры")]
        public float SpeedMove = 2f;

        [Header("Связь с режимом стрельбы")]
        public Mode Mode;

        [Header("Обновить")]
        public bool IsUpDate = false;

    }
}

