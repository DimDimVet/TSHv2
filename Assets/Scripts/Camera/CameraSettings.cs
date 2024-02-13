using Input;
using UnityEngine;

namespace CameraMain
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [Header("��������� �� ��� X")]
        public float AxesX = 0f;
        [Header("��������� �� ��� Y")]
        public float AxesY = 0f;
        [Header("��������� �� ��� Z")]
        public float AxesZ = -30f;

        public Vector3 GetAxes()
        {
            return new Vector3(AxesX, AxesY, AxesZ);
        }

        [Header("�������� ����������� ������")]
        public float SpeedMove = 2f;

        [Header("����� � ������� ��������")]
        public Mode Mode;

        [Header("��������")]
        public bool IsUpDate = false;

    }
}

