using Input;
using UnityEngine;

namespace AudioScene
{
    [CreateAssetMenu(fileName = "AudioClipSetting", menuName = "ScriptableObjects/AudioClipSetting")]
    public class AudioClipSetting : ScriptableObject
    {
        [Header("Звуковой файл ")]
        public AudioClip AudioClip;
        [Header("Режим ")]
        public Mode Mode;

    }
}
