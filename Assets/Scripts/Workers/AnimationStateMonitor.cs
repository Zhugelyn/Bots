using System;
using UnityEngine;

namespace Workers
{
    public class AnimationStateMonitor : MonoBehaviour
    {
        private const int LayerIndex = 0;
        private const float CompletitionTime = 1f;

        public event Action OnPickUpFinished;

        private Animator _animator;
        private AnimatorStateInfo _stateInfo;

        private void Update()
        {
            CheckFinished();
        }

        public void Initialize(Animator animator, Action onPickUpFinished = null)
        {
            _animator = animator;
            OnPickUpFinished = onPickUpFinished;
        }

        public void CheckFinished()
        {
            _stateInfo = _animator.GetCurrentAnimatorStateInfo(LayerIndex);

            if (_stateInfo.shortNameHash == WorkerAnimatorData.Params.IsPickUp &&
                _stateInfo.normalizedTime >= CompletitionTime)
                OnPickUpFinished?.Invoke();
        }
    }
}