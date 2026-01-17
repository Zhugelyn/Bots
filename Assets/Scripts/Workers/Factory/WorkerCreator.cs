using System;
using UnityEngine;

namespace Workers.Factory
{
    public class WorkerCreator : UniversalObjectPool<Worker>
    {
        [SerializeField] private Base _base;
        [SerializeField] private Transform _spawnPosition;

        private int _startWorkersCount = 3;
        
        public event Action<Worker> WorkerCreated;
        
        private void Start()
        {
            for (int i = 0; i < _startWorkersCount; i++)
                Create();
        }
        
        private void Create()
        {
            Worker worker = Pool.Get();
            worker.Initialize(_base.GatheringPointWorkers.position, _spawnPosition.position);
            WorkerCreated?.Invoke(worker);
        }
    }
}
