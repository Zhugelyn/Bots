using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Workers;

public class BaseCommander : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Scanner _scanner;

    [SerializeField] private List<Vector3> _resourcePosition;

    public void Initialize()
    {
        _resourcePosition = new List<Vector3>();

        _scanner.ResourcesFound += AddPositions;
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnDisable()
    {
        _scanner.ResourcesFound -= AddPositions;
    }

    private void Update()
    {
        if (_resourcePosition.Any())
            SendWorkerToGetResource(_resourcePosition.First());
    }

    private void SendWorkerToGetResource(Vector3 position)
    {
        var worker = _base.GetFreeWorker();

        if (worker == null)
            return;
        
        _resourcePosition.Remove(position);
    }

    private void AddPositions(List<Vector3> resources) => 
        _resourcePosition.AddRange(resources);
}