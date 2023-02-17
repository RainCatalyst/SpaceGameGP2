using System;
using UnityEngine;

namespace SpaceGame
{
    public class RepairInteractable : Interactable
    {
        public event Action Repaired;
        public event Action Exploded;
        public event Action Damaged;
        public bool IsRepaired => _isRepaired;

        public void Repair(bool ignoreEvents = false)
        {
            _isRepaired = true;
            _meshRenderer.material = _repairedMaterial;
            // _escalationBar.gameObject.SetActive(false);
            if (_sparks != null)
                _sparks.SetActive(false);
            if (!ignoreEvents)
                Repaired?.Invoke();
        }

        public void Break()
        {
            _isRepaired = false;
            _meshRenderer.material = _defaultMaterial;
            // _escalationBar.gameObject.SetActive(true);
            //ParticleManager.Instance.Spawn(ParticleType.Explosion, _effectOrigin.position);
            if (_sparks != null)
                _sparks.SetActive(true);
            Damaged?.Invoke();
            _breakSound.Play();
        }
        
        public override bool CanInteract(Interactor interactor)
        {
            //Seth edit
            return base.CanInteract(interactor) && !_isRepaired && interactor.ItemHolder.ItemId == "wrench";
        }

        protected override void OnInteractionFinished()
        {
            Repair();
            //Seth edit
           // _currentInteractor.ItemHolder.SetItem(null);
            base.OnInteractionFinished();
        }

        private void Awake()
        {
            _defaultMaterial = _meshRenderer.material;
            Repair(true);
        }

        protected override void Update()
        {
            base.Update();
            // if (!_isRepaired)
            // {
            //     _timer += Time.deltaTime;
            //     _escalationBar.Progress = 1f - _timer / _timeUntilFailure;
            //     if (_timer >= _timeUntilFailure)
            //     {
            //         _timer = 0;
            //         Exploded?.Invoke();
            //     }
            // }
        }
        
        [SerializeField] private bool _isRepaired;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _repairedMaterial;
        [SerializeField] private ProgressBar _escalationBar;
        [Header("Escalation")]
        [SerializeField] private float _timeUntilFailure = 10f;
        [SerializeField] private int _damage = 1;
        [Header("Effects")]
        //[SerializeField] private Transform _effectOrigin;
        [SerializeField] private GameObject _sparks;
        [SerializeField] private AudioClipSO _breakSound;
        
        private float _timer;
        private Material _defaultMaterial;
    }
}