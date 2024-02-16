using Input;
using System;

namespace AudioScene
{
    public interface IAudioShootExecutor
    {
        Action<int, Mode> OnShootAudio { get; set; }

        void ShootAudio(int thisHash, Mode mode);
    }
}