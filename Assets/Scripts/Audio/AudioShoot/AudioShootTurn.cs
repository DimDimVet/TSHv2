using Input;
using UnityEngine;

namespace AudioScene
{
    public class AudioShootTurn : AudioShoot
    {
        public override void ShootAudio(int _thisHash, Mode mode)
        {
            if (ThisHash == _thisHash && Mode == Mode.Turn)
            {
                AudioSource.clip = AudioClipSetting.AudioClip;
                AudioSource.Play();
            }
        }
    }
}