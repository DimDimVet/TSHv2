using UnityEngine;

namespace Effect
{
    public class ParticleDecal : MonoBehaviour
    {
        private ParticleSystem particle;

        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            if (particle == null) { particle = GetComponent<ParticleSystem>(); }

        }
    }
}

