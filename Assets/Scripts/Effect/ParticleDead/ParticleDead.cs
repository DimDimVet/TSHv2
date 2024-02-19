using Healt;
using UnityEngine;
using Zenject;

namespace Effect
{
    public class ParticleDead : MonoBehaviour
    {
        [SerializeField] private ParticleSystem partDead;
        private int thisHash;
        private bool isPart = true, isDead = false;
        private bool isStopClass = false, isRun = false;

        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor)
        {
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnIsDead += IsDead;
        }
        private void IsDead(int getHash, bool _isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash) { isDead = _isDead; }
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

