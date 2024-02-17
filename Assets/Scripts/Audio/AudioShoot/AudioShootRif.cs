using Input;
using UnityEngine;

namespace AudioScene
{
    public class AudioShootRif : AudioShoot
    {
        public override void ShootAudio(int _thisHash, Mode mode)
        {
            if (ThisHash == _thisHash && mode == Mode.AvtoRif )
            {
                AudioSource.Play();
            }
        }
    }
}

