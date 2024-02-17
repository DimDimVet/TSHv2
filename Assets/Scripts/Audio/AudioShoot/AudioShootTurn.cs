using Input;
using UnityEngine;

namespace AudioScene
{
    public class AudioShootTurn : AudioShoot
    {
        public override void ShootAudio(int _thisHash, Mode mode)
        {
            if (ThisHash == _thisHash && mode == Mode.Turn )
            {
                AudioSource.Play();
            }
        }
    }
}