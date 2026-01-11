using System;
using UnityEngine;

namespace Workers
{
    [RequireComponent(typeof(AnimationStateMonitor))]
    public class WorkerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public event Action PickUpFinished;

        private AnimationStateMonitor _monitor;

        private void Awake()
        {
            _monitor = GetComponent<AnimationStateMonitor>();
            _monitor.Initialize(_animator, OnPickUpComplete);
        }

        private void OnPickUpComplete()
        {
            PickUpFinished?.Invoke();
        }

        private void Setup(float speed, bool isPickUp)
        {
            _animator.SetFloat(WorkerAnimatorData.Params.Speed, speed);
            _animator.SetBool(WorkerAnimatorData.Params.IsPickUp, isPickUp);
        }

        public void Move()
        {
            Setup(1f, false);
        }

        public void PickUp()
        {
            Setup(0f, true);
        }

        public void Idle()
        {
            Setup(0f, false);
        }
    }
}