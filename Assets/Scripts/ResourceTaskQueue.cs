using System.Collections.Generic;
using UnityEngine;

public class ResourceTaskQueue : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    
    private Queue<Vector3> _tasks;
    private List<Vector3> _assignedPositions;
    
    public bool HasTasks => _tasks.Count > 0;

    private void Awake()
    {
        _tasks = new Queue<Vector3>();
        _assignedPositions = new List<Vector3>();
    }

    private void OnEnable()
    {
        _scanner.ResourcesFound += AddTasks;
    }

    private void OnDisable()
    {
        _scanner.ResourcesFound -= AddTasks;
    }
    
    public Vector3 GetNext()
    {
        Vector3 position = _tasks.Dequeue();
        _assignedPositions.Add(position);
        return position;
    }

    private void AddTasks(List<Vector3> positions)
    {
        _assignedPositions.RemoveAll(assigned => positions.Contains(assigned) == false);

        foreach (var position in positions)
        {
            if (!_tasks.Contains(position) && !_assignedPositions.Contains(position))
                _tasks.Enqueue(position);
        }
    }
}

