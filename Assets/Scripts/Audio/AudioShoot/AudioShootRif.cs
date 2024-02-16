using Input;
using UnityEngine;

namespace AudioScene
{
    public class AudioShootRif : AudioShoot
    {
        public override void ShootAudio(int _thisHash, Mode mode)
        {
            if (ThisHash == _thisHash && Mode==Mode.AvtoRif)
            {
                AudioSource.clip = AudioClipSetting.AudioClip;
                AudioSource.Play();
            }
        }
    }
}

