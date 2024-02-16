using Input;
using UnityEngine;

namespace AudioScene
{
    [CreateAssetMenu(fileName = "AudioClipSetting", menuName = "ScriptableObjects/AudioClipSetting")]
    public class AudioClipSetting : ScriptableObject
    {
        [Header("�������� ���� ")]
        public AudioClip AudioClip;
        [Header("����� ")]
        public Mode Mode;

    }
}
