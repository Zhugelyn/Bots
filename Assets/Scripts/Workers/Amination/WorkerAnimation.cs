using System;
using UnityEngine;

namespace Workers
{
    [RequireComponent(typeof(AnimationStateMonitor))]
    public class WorkerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimationStateMonitor _monitor;

        public event Action OnPickUpCompleted;

        public bool IsPickUpComplete { get; private set; }

        private void Awake()
        {
            _monitor = GetComponent<AnimationStateMonitor>();
            _monitor.Initialize(_animator, OnPickUpComplete);
        }

        private void OnPickUpComplete()
        {
            IsPickUpComplete = true;
            OnPickUpCompleted?.Invoke();
        }

        public void Move()
        {
            Setup(1f, false, false);
        }

        public void PickUp()
        {
            IsPickUpComplete = false;
            Setup(0f, true, false);
        }

        public void Idle()
        {
            Setup(0f, false, false);
        }

        public void Build()
        {
            Setup(0f, false, true);
        }

        private void Setup(float speed, bool isPickUp, bool isBuild)
        {
            _animator.SetFloat(WorkerAnimatorData.Params.Speed, speed);
            _animator.SetBool(WorkerAnimatorData.Params.IsPickUp, isPickUp);
            _animator.SetBool(WorkerAnimatorData.Params.IsBuild, isBuild);
        }
    }
}
