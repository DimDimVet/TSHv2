using Input;

namespace AudioScene
{
    public class AudioShootTurn : AudioShoot
    {
        protected override void ShootAudio(int _thisHash, Mode mode)
        {
            if (thisHash == _thisHash && mode == Mode.Turn)
            {
                audioSource.Play();
            }
        }
    }
}