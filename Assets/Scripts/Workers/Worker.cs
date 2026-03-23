using System;
using System.Collections;
using UnityEngine;

namespace Workers
{
    public class Worker : MonoBehaviour
    {
        private const float BuildAnimationOffsetY = 1.6f;

        [SerializeField] private Transform _resourceCarryPoint;
        [SerializeField] private BotMovement _movement;
        [SerializeField] private BotRotation _rotation;
        [SerializeField] private WorkerAnimation _animation;

        private Transform _homeBase;
        private Coroutine _currentMission;
        private bool _isPickUpDone;

        public event Action<Worker, Vector3> BuildCompleted;

        public Resource CarriedResource { get; private set; }
        public bool IsBusy { get; private set; }

        public void Initialize(Transform homeBase)
        {
            _homeBase = homeBase;
            transform.position = homeBase.position;
            IsBusy = false;
            CarriedResource = null;
        }

        public void SetHomeBase(Transform homeBase)
        {
            _homeBase = homeBase;
        }

        public void SendToCollect(Resource resource)
        {
            if (_currentMission != null)
                StopCoroutine(_currentMission);

            _currentMission = StartCoroutine(Collect(resource));
        }

        public void SendToBuild(Vector3 buildPosition)
        {
            if (_currentMission != null)
                StopCoroutine(_currentMission);

            _currentMission = StartCoroutine(Build(buildPosition));
        }

        public void ReturnToBase()
        {
            if (_currentMission != null)
                StopCoroutine(_currentMission);

            _currentMission = StartCoroutine(GoToBase());
        }

        public void StopMission()
        {
            if (_currentMission != null)
            {
                StopCoroutine(_currentMission);
                _currentMission = null;
            }

            IsBusy = false;
            _animation.Idle();
        }

        public Resource TakeResource()
        {
            var resource = CarriedResource;
            resource.transform.SetParent(null);
            CarriedResource = null;
            return resource;
        }

        private IEnumerator Collect(Resource resource)
        {
            IsBusy = true;

            _animation.Move();
            yield return _rotation.SmoothLookAt(resource.transform);
            yield return _movement.MoveTo(resource.transform);

            if (resource.gameObject.activeInHierarchy == false)
            {
                IsBusy = false;
                yield break;
            }

            resource.AttachTo(_resourceCarryPoint);
            CarriedResource = resource;

            _isPickUpDone = false;
            _animation.OnPickUpCompleted += OnPickUpComplete;
            _animation.PickUp();

            yield return new WaitUntil(() => _isPickUpDone);
            _animation.OnPickUpCompleted -= OnPickUpComplete;

            _animation.Move();

            yield return _rotation.SmoothLookAt(_homeBase);
            yield return _movement.MoveTo(_homeBase);

            IsBusy = false;
        }

        private IEnumerator GoToBase()
        {
            IsBusy = true;
            _animation.Move();
            yield return _rotation.SmoothLookAt(_homeBase);
            yield return _movement.MoveTo(_homeBase);
            IsBusy = false;
        }

        private IEnumerator Build(Vector3 buildPosition)
        {
            IsBusy = true;

            yield return _rotation.SmoothLookAt(buildPosition);
            yield return _movement.MoveTo(buildPosition);

            transform.position += Vector3.up * BuildAnimationOffsetY;
            _animation.Build();

            BuildCompleted?.Invoke(this, buildPosition);
        }

        private void OnPickUpComplete() => _isPickUpDone = true;
    }
}
