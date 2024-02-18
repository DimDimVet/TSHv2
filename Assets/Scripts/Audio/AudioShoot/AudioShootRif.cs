using Input;

namespace AudioScene
{
    public class AudioShootRif : AudioShoot
    {
        protected override void ShootAudio(int _thisHash, Mode mode)
        {
            if (thisHash == _thisHash && mode == Mode.AvtoRif)
            {
                audioSource.Play();
            }
        }
    }
}

