using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WorkerCreator : UniversalObjectPool<Worker>
{
    [SerializeField] private Base _base;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Button _button;

    private int _startWorkersCount = 3;

    private void Start()
    {
        while (_startWorkersCount != 0)
        {
            CreateWorker();
            _startWorkersCount--;
        }
    }

    public event Action<Worker> WorkerCreated;

    private void OnEnable()
    {
        _button.onClick.AddListener(CreateWorker);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CreateWorker);
    }

    private void CreateWorker()
    {
        var worker = Pool.Get();
        worker.Initialize(_base, _spawnPosition.position);
        WorkerCreated?.Invoke(worker);
    }
}
