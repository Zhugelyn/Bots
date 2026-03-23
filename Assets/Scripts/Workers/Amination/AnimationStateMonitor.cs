using System;
using UnityEngine;

namespace Workers
{
    public class AnimationStateMonitor : MonoBehaviour
    {
        private const int LayerIndex = 0;
        private const float CompletionTime = 1f;

        private Animator _animator;
        private Action _onPickUpFinished;

        public void Initialize(Animator animator, Action onPickUpFinished = null)
        {
            _animator = animator;
            _onPickUpFinished = onPickUpFinished;
        }

        private void Update()
        {
            if (_animator == null)
                return;

            if (_animator.GetBool(WorkerAnimatorData.Params.IsPickUp) == false)
                return;

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(LayerIndex);

            if (stateInfo.normalizedTime >= CompletionTime)
                _onPickUpFinished?.Invoke();
        }
    }
}
