using System;
using UnityEngine;

namespace Workers.Factory
{
    public class WorkerCreator : UniversalObjectPool<Worker>
    {
        [SerializeField] private Transform _basePosition;

        private int _startWorkersCount = 3;
        
        public event Action<Worker> WorkerCreated;
        
        public void CreateStartWorkers()
        {
            for (int i = 0; i < _startWorkersCount; i++)
                Create();
        }
        
        public void Create()
        {
            Worker worker = Pool.Get();
            worker.Initialize(_basePosition.position);
            WorkerCreated?.Invoke(worker);
        }
    }
}
