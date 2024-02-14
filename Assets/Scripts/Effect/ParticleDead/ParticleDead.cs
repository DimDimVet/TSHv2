using UnityEngine;

namespace Effect
{
    public class ParticleDead : MonoBehaviour
    {
        [SerializeField] private ParticleSystem partDead;
        private int thisHash;
        private bool isPart = true, isDead=false;
        private bool isStopClass = false, isRun = false;

        private void OnEnable()
        {
            isDead = false;
            //OnIsDead += StopRun;
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                thisHash = this.gameObject.GetHashCode();
                partDead.Stop();

                if (partDead != null) { isRun = true; }
                else { isRun = false; }
            }
        }

        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            if (isDead)
            {
                if (isPart == true)
                {
                    PartDead(isPart);
                    isPart = false;
                }
            }
            else
            {
                if (isPart == false)
                {
                    isPart = true;
                }
            }
        }
        private void PartDead(bool isActiv)
        {
            if (isActiv)
            {
                partDead.Play();
            }
            else
            {
                partDead.Stop();
            }
        }

    }
}

