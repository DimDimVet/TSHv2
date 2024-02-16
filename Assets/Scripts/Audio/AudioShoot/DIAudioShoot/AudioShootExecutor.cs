using Input;
using System;

namespace AudioScene
{
    //public enum TypeAydio
    //{
    //    PlayerTurn,
    //    PlayerRif,
    //    EnemyTank,
    //    EnemyAvto
    //}
    public class AudioShootExecutor : IAudioShootExecutor
    {
        public Action<int, Mode> OnShootAudio { get { return onShootAudio; } set { onShootAudio = value; } }
        private Action<int, Mode> onShootAudio;

        public void ShootAudio(int thisHash, Mode mode)
        {
            onShootAudio?.Invoke(thisHash,mode);
        }
    }
}
